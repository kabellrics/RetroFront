using RetroFront.Models.ScreenScraper.GameSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetroFrontAPIService.Service
{
    public class ThemePath
    {
        public string Path { get; set; }
        public ThemePath(string path)
        {
            if (!string.IsNullOrEmpty(path))
                Path = path;
            else
                Path = string.Empty;
        }
    }
    public class CopyAndDLLObject
    {
        public string Source { get; set; }
        public string Dest { get; set; }
        public CopyAndDLLObject(string source, string dest)
        {
            {
                if (!string.IsNullOrEmpty(source))
                    Source = source;
                else
                    Source = string.Empty;

                if (!string.IsNullOrEmpty(dest))
                    Dest = dest;
                else
                    Dest = string.Empty;

            }
        }
    }
    public class DLLByteObject
    {
        public byte[] Source { get; set; }
        public string Dest { get; set; }
        public DLLByteObject(string dest, byte[] source = null)
        {
            {
                    Source = source;
                if (!string.IsNullOrEmpty(dest))
                    Dest = dest;
                else
                    Dest = string.Empty;

            }
        }
    }

    public class SCGamedetail
    {
        public String editeur { get; set; }
        public String developpeur { get; set; }
        public String synopsis { get; set; }
        public String genres { get; set; }
        public String Year { get; set; }

        public SCGamedetail(Jeux jeux)
        {
            this.editeur = jeux.editeur.text;
            this.developpeur = jeux.developpeur.text;
            this.synopsis = jeux.synopsis.FirstOrDefault(x => x.langue == "fr").text;
            var listgenre = jeux.genres.SelectMany(x => x.noms.Where(n => n.langue == "fr"));
            genres = string.Join(", ",listgenre.Select(x=>x.text));
            this.Year = jeux.dates.FirstOrDefault(x => x.region == "fr" || x.region == "eu").text;
        }
    }
}
