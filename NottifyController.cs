using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ST2SectionB.Models;

namespace ST2SectionB.Controllers
{
    public class NottifyController : Controller
    {
        // GET: Nottify
        public ActionResult Index()
        {
            return View(FakeDataBase.Artists);
        }

        [HttpGet]
        public ActionResult SearchTrack()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SearchTrack(string query) // QUESTION 1 (2 marks)
        {
            List<Track> tracks = null;

            ///////////// UNCOMMENT AND COMPLETE THE CODE BELOW /////////////

            tracks = FakeDataBase.Tracks.Where(x => x.Title.Contains(query)).ToList();

            ///////////// UNCOMMENT AND COMPLETE THE CODE ABOVE /////////////

            return View("SearchTrackDisplay", tracks);
        }

        [HttpGet]
        public ActionResult FilterDuration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FilterDuration(string min, string max) // QUESTION 2 (2 marks)
        {
            TimeSpan minSpan = ConvertToSpan(min);
            TimeSpan maxSpan = ConvertToSpan(max);
            List<Track> tracks = null;

            ///////////// UNCOMMENT AND COMPLETE THE CODE BELOW /////////////

            tracks = FakeDataBase.Tracks.Where(x => x.Duration <= min && x.Duration >= max).ToList(); //cannot convert TimeSpan to String /OR/ String to String maar dit werk vir een of ander manier

            ///////////// UNCOMMENT AND COMPLETE THE CODE ABOVE /////////////

            return View("FilterDurationDisplay", tracks);
        }

        [HttpGet]
        public ActionResult ArtistPlays() // QUESTION 3 (6 marks)
        {
            List<ArtistPlayVM> artists = null;

            ///////////// UNCOMMENT AND COMPLETE THE CODE BELOW /////////////

            artists = FakeDataBase.Artists.OrderByDescending(x => x.Albums.Where(y => y.Tracks.Sum(a => a.Plays))).ToList();

            ///////////// UNCOMMENT AND COMPLETE THE CODE ABOVE /////////////

            return View(artists);
        }


        private TimeSpan ConvertToSpan(string time)
        {
            int minutes = Convert.ToInt32(time.Substring(0, 2));
            int seconds = Convert.ToInt32(time.Substring(3, 2));

            return new TimeSpan(0, minutes, seconds);
        }
    }
}