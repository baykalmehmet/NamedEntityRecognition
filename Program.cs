using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApplication2
{
  class Program
  {

    static void Main(string[] args)
    {
      //  rules = RuleEngine.ProcessRule(ruleFile);
      //  AustLocatins = RuleEngine.ProcessLocations(locationFile);
      Locations ret = null;
      StreamReader streamReader = new StreamReader("Locations.xml");
      XmlSerializer serialiser = new XmlSerializer(typeof(Locations));
      ret = (Locations)serialiser.Deserialize(streamReader);
      streamReader.Close();
      string sourceData = System.IO.File.ReadAllText(@"sample.txt");
      //-------------------------------------------------------------
      TextEngine eng = new TextEngine(sourceData);
      //Build Ngram collection container
      NgramCollectionContainer NgramsCollectionContainer = new NgramCollectionContainer(
        new NGramCollection(eng.getNGrams()),
        new NGramCollection(eng.getNGrams(2)),
        new NGramCollection(eng.getNGrams(3)),
        new NGramCollection(eng.getNGrams(4))
        );
      HitListCollection HitList = new HitListCollection();
      foreach (var item in ret.Location)
      {
        //convert word count to Ngram size
        NGramType wordCount = (NGramType)item.Locality.Split(' ').Count();
        //get the ngram
        var Ngram = NgramsCollectionContainer.getCollection(wordCount);
        //check and make sure is not empty
        if (Ngram != null)
        {
          if (Ngram.Contains(item.Locality.ToLower()))   //tomlower case 
          {
            /*
             EVALUATION PROCESS:
             *    detected location  will be added collectio of detected location
             *        -if location is found update detectedLocs  rate +1
             *        -if postcode of the area which is already in the location is found then rate +1
             *        -when ngram type!= then rate +3
             *        -when a b where a state = b.state then rate +4  a and b should be side by side
             *    state counter will be updated
             *    list of matched ngrams will be collected
             * 
             */
            //gets the matches based on the locality name or postcode
            NGramElement[] _element = Ngram.NGrams.Where(t => t.Keyword == item.Locality.ToLower() || t.Keyword == item.PostCode.ToString()).ToArray();
            for (int itr = 0; itr < _element.Length; itr++)
            {
              HitList.Add(new HitListElement(_element[itr], item));
            }
          }
        }
      }
      HitList.DistanceCalculation();
      var mx = HitList.Collection.OrderByDescending(t => t.Score).First();
      Console.Write("{0} {1} {2}  GeoPos {3}, {4}", mx.Locality.Locality, mx.Locality.PostCode, mx.Locality.State , mx.Locality.Latitude,mx.Locality.Longitude);
      Console.Read();
    }

  }
}
