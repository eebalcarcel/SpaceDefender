using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SpaceDefender.Models;

namespace SpaceDefender.Managers
{
    public class ScoreManager
    {
        private static string _fileName = "scores.xml";

        public List<Score> HighScores { get; private set; }

        public List<Score> Scores { get; private set; }

        public ScoreManager()
          : this(new List<Score>())
        {

        }    

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;

            UpdateHighscores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);

            Scores = Scores.OrderByDescending(c => c.Value).ToList();

            UpdateHighscores();
        }

        public static ScoreManager Load()
        {
            if (!File.Exists(_fileName))
                return new ScoreManager();

            using (StreamReader reader = new StreamReader(new FileStream(_fileName, FileMode.Open)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Score>));

                List<Score> scores = (List<Score>)serializer.Deserialize(reader);

                return new ScoreManager(scores);
            }
        }

        public void UpdateHighscores()
        {
            HighScores = Scores.Take(10).ToList();
        }

        public static void Save(ScoreManager scoreManager)
        {
            using (StreamWriter writer = new StreamWriter(new FileStream(_fileName, FileMode.Create)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Score>));

                serializer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
