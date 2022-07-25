using RetroFront.UWPAdmin.Core.APIClient;
using RetroFront.UWPAdmin.Core.APIHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroFront.UWPAdmin.Core.Services
{
    public class GameDetailModalService
    {
        private GameDataProviderClient gameDataProvider;

        public GameDetailModalService()
        {
            gameDataProvider = new GameDataProviderClient();
        }

        public async Task<List<Search>> ResolveByName(String Name, ScraperSource Source) 
        {
            var FoundedGame = new List<Search>();
            if (Source == ScraperSource.SGDB)
            {
                var games = await gameDataProvider.SearchByNameAsync(Name);
                FoundedGame = games.Result.ToList();
            }
            else if (Source == ScraperSource.IGDB)
            {
                var games = await gameDataProvider.GetGameByNameAsync(Name);
                FoundedGame = games.Result.ToList();
            }
            else if (Source == ScraperSource.Screenscraper)
            {
                var games = await gameDataProvider.SearchGameAsync(Name);
                var SCSPFoundedGame = games.Result.ToList();
                foreach (var SCSPgame in SCSPFoundedGame)
                {
                    FoundedGame.Add(new Search() { Id = SCSPgame.ScreenScraperID, Name = SCSPgame.Name });
                }
            }
            return FoundedGame;
        }

        public async Task<List<String>> GetImgProposal(int gameId, ScraperSource CurrentScrapeSource, ScraperType CurrentScraperType)
        {
            var ResultImgs = new List<String>();
            if (CurrentScraperType == ScraperType.Logo)
            {
                if (CurrentScrapeSource == ScraperSource.SGDB)
                {
                    var result = gameDataProvider.GetLogoForId(gameId);
                    if (result != null)
                    {
                        ResultImgs.AddRange(result.Result.Select(x=>x.Url));
                    }
                }
                else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                {
                    var result = await gameDataProvider.GetJeuxMediasAsync(gameId);
                    if (result != null)
                    {
                        foreach (var img in result.Where(x => x.Type == "wheel"))
                        {
                            ResultImgs.Add(img.Url);
                        }
                    }
                }
            }
            else if (CurrentScraperType == ScraperType.ArtWork)
            {
                IEnumerable<ImgResult> result;
                if (CurrentScrapeSource == ScraperSource.SGDB)
                {
                     result = gameDataProvider.GetHeroesForId(gameId).Result;
                    if (result != null)
                    {
                        ResultImgs.AddRange(result.Select(x => x.Url));
                    }
                }
                else if (CurrentScrapeSource == ScraperSource.IGDB)
                {                    
                    var detailart = gameDataProvider.GetArtworksByGameId(gameId).Result;
                    var detailsch = gameDataProvider.GetScreenshotsByGameId(gameId).Result;
                    if (detailart != null)
                    {
                        var resultartigdb = detailart.Select(x => x.Url);
                        ResultImgs.AddRange(resultartigdb);
                    }
                    if (detailsch != null)
                    {
                        var resultschigdb = detailsch.Select(x => x.Url);
                        ResultImgs.AddRange(resultschigdb);
                    }
                }
                else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                {
                    var arts = await gameDataProvider.GetJeuxMediasAsync(gameId);
                    if (arts != null)
                    {
                        foreach (var img in arts.Where(x => x.Type == "fanart" || x.Type == "ss" || x.Type == "sstitle" || x.Type == "themehs"))
                        {
                            ResultImgs.Add(img.Url);
                        }
                    }
                }
            }
            else if (CurrentScraperType == ScraperType.Banner)
            {
                if (CurrentScrapeSource == ScraperSource.SGDB)
                {
                    var result = gameDataProvider.GetGridBannerForId(gameId);
                    if (result != null)
                    {
                        ResultImgs.AddRange(result.Result.Select(x=>x.Url));
                        //foreach (var img in result)
                        //{
                        //    ResultImgs.Add(img.url);
                        //}
                    }
                }
                else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                {
                    var arts = await gameDataProvider.GetJeuxMediasAsync(gameId);
                    if (arts != null)
                    {
                        foreach (var img in arts.Where(x => x.Type == "marquee" || x.Type == "screenmarquee" || x.Type == "steamgrid"))
                        {
                            ResultImgs.Add(img.Url);
                        }
                    }
                }
            }
            else if (CurrentScraperType == ScraperType.Boxart)
            {
                if (CurrentScrapeSource == ScraperSource.SGDB)
                {
                    var result = gameDataProvider.GetGridBoxartForId(gameId);
                    if (result != null)
                    {
                        ResultImgs.AddRange(result.Result.Select(x => x.Url));
                        //foreach (var img in result)
                        //{
                        //    ResultImgs.Add(img.url);
                        //}
                    }
                }
                else if (CurrentScrapeSource == ScraperSource.IGDB)
                {
                    var detail = gameDataProvider.GetDetailsGame(gameId);
                    ResultImgs.Add(detail.Result.Cover.Url);
                }
                else if (CurrentScrapeSource == ScraperSource.Screenscraper)
                {
                    var arts = await gameDataProvider.GetJeuxMediasAsync(gameId);
                    if (arts != null)
                    {
                        foreach (var img in arts.Where(x => x.Type == "box-2D"))
                        {
                            ResultImgs.Add(img.Url);
                        }
                    }
                }
            }
            return ResultImgs;
        }
    }
}
