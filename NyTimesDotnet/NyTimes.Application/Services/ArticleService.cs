using Microsoft.Extensions.Configuration;
using NyTimes.Domain.Models;
using NyTimes.Domain.ViewModels;
using System.Net.Http;
using System.Text.Json;

namespace NyTimes.Application.Services
{
    public class ArticleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        public ArticleService(IUnitOfWork unitOfWork, HttpClient httpClient, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<IEnumerable<Articles>> GetArticlesAsync()
        {

            var articles = await _unitOfWork.Repository<Articles>().GetAllAsync();
            return articles.OrderByDescending(a => a.Id).ToList();

        }

        public async Task<Articles> GetArticleByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Articles>().GetByIdAsync(id);
        }

        public async Task AddArticleAsync(Articles article)
        {
            await _unitOfWork.Repository<Articles>().AddAsync(article);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateArticleAsync(Articles article)
        {
            _unitOfWork.Repository<Articles>().Update(article);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = await _unitOfWork.Repository<Articles>().GetByIdAsync(id);
            if (article != null)
            {
                _unitOfWork.Repository<Articles>().Delete(article);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private DateTime? ValidateDate(DateTime? date)
        {
            if (!date.HasValue || date.Value < new DateTime(1753, 1, 1))
                return null;
            return date;
        }


        public async Task<string> FetchArticlesByKeyAsync(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("API key is required.");
            }

            //var nytimesApiUrl = _config.GetSection("nytimesApiUrl").Get<string>() ;

            string apiUrl = $"https://api.nytimes.com/svc/topstories/v2/home.json?api-key={apiKey}";

            try
            {
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error fetching news data. Status Code: {response.StatusCode}");
                }

                var responseString = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                    PropertyNameCaseInsensitive = true
                };

                var jsonObject = JsonSerializer.Deserialize<HomeResponseViewModel>(responseString, options);


                foreach (var article in jsonObject.Results)
                {
                    var newArticle = new Articles
                    {
                        Section = article.Section,
                        Subsection = article.Subsection,
                        Title = article.Title,
                        Abstract = article.Abstract,
                        Url = article.Url,
                        Byline = article.Byline,
                        UpdatedDate = ValidateDate(article.UpdatedDate),
                        CreatedDate = ValidateDate(article.CreatedDate),
                        PublishedDate = ValidateDate(article.PublishedDate),
                        Facets = new List<Facets>(),
                        Multimedia = new List<Multimedia>()
                    };

                    // Insert Facets (des_facet, org_facet, per_facet, geo_facet)
                    foreach (var facet in article.DesFacet ?? new List<string>())
                        newArticle.Facets.Add(new Facets { FacetType = "des_facet", FacetValue = facet });

                    foreach (var facet in article.OrgFacet ?? new List<string>())
                        newArticle.Facets.Add(new Facets { FacetType = "org_facet", FacetValue = facet });

                    foreach (var facet in article.PerFacet ?? new List<string>())
                        newArticle.Facets.Add(new Facets { FacetType = "per_facet", FacetValue = facet });

                    foreach (var facet in article.GeoFacet ?? new List<string>())
                        newArticle.Facets.Add(new Facets { FacetType = "geo_facet", FacetValue = facet });

                    // Insert Multimedia
                    foreach (var media in article.Multimedia ?? new List<MultimediaViewModel>())
                    {
                        newArticle.Multimedia.Add(new Multimedia
                        {
                            Url = media.Url,
                            Format = media.Format,
                            Height = media.Height,
                            Width = media.Width,
                            Type = media.Type,
                            Subtype = media.Subtype,
                            Caption = media.Caption,
                            Copyright = media.Copyright
                        });
                    }

                    await _unitOfWork.Repository<Articles>().AddAsync(newArticle);
                }

                await _unitOfWork.SaveChangesAsync();

                return responseString;

            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

    }



}
