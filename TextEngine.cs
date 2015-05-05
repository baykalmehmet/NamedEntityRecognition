using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
  class TextEngine
  {

    /// <summary>
    /// This is to be used for NGrams generation
    /// </summary>
    public string Text2BNormalized { get; private set; }


    /// <summary>
    /// stopwords to refine any given text
    /// </summary>
    string[] stopWords = {
                    #region stopwords
		                                 "a's", "able", "about", "above", "according", "accordingly", "across", "actually", "after", "afterwards", "again", "against", "ain't", "all", "allow", "allows", "almost", "alone", "along", "already", "also", "although", "always", "am", "among", "amongst", "an", "and", "another", "any", "anybody", "anyhow", "anyone", "anything", "anyway", "anyways", "anywhere", "apart", "appear", "appreciate", "appropriate", "are", "aren't", "around", "as", "aside", "ask", "asking", "associated", "at", "available", "away", "awfully", "b", "be", "became", "because", "become", "becomes", "becoming", "been", "before", "beforehand", "behind", "being", "believe", "below", "beside", "besides", "best", "better", "between", "beyond", "both", "brief", "but", "by", "c", "c'mon", "c's", "came", "can", "can't", "cannot", "cant", "cause", "causes", "certain", "certainly", "changes", "clearly", "co", "com", "come", "comes", "concerning", "consequently", "consider", "considering", "contain", "containing", "contains", "corresponding", "could", "couldn't", "course", "currently", "d", "definitely", "described", "despite", "did", "didn't", "different", "do", "does", "doesn't", "doing", "don't", "done", "down", "downwards", "during", "e", "each", "edu", "eg", "eight", "either", "else", "elsewhere", "enough", "entirely", "especially", "et", "etc", "even", "ever", "every", "everybody", "everyone", "everything", "everywhere", "ex", "exactly", "example", "except", "f", "far", "few", "fifth", "first", "five", "followed", "following", "follows", "for", "former", "formerly", "forth", "four", "from", "further", "furthermore", "g", "get", "gets", "getting", "given", "gives", "go", "goes", "going", "gone", "got", "gotten", "greetings", "h", "had", "hadn't", "happens", "hardly", "has", "hasn't", "have", "haven't", "having", "he", "he's", "hello", "help", "hence", "her", "here", "here's", "hereafter", "hereby", "herein", "hereupon", "hers", "herself", "hi", "him", "himself", "his", "hither", "hopefully", "how", "howbeit", "however", "i", "i'd", "i'll", "i'm", "i've", "ie", "if", "ignored", "immediate", "in", "inasmuch", "inc", "indeed", "indicate", "indicated", "indicates", "inner", "insofar", "instead", "into", "inward", "is", "isn't", "it", "it'd", "it'll", "it's", "its", "itself", "j", "just", "k", "keep", "keeps", "kept", "know", "knows", "known", "l", "last", "lately", "later", "latter", "latterly", "least", "less", "lest", "let", "let's", "like", "liked", "likely", "little", "look", "looking", "looks", "ltd", "m", "mainly", "many", "may", "maybe", "me", "mean", "meanwhile", "merely", "might", "more", "moreover", "most", "mostly", "much", "must", "my", "myself", "n", "name", "namely", "nd", "near", "nearly", "necessary", "need", "needs", "neither", "never", "nevertheless", "next", "nine", "no", "nobody", "non", "none", "noone", "nor", "normally", "not", "nothing", "novel", "now", "nowhere", "o", "obviously", "of", "off", "often", "oh", "ok", "okay", "old", "on", "once", "one", "ones", "only", "onto", "or", "other", "others", "otherwise", "ought", "our", "ours", "ourselves", "out", "outside", "over", "overall", "own", "p", "particular", "particularly", "per", "perhaps", "placed", "please", "plus", "possible", "presumably", "probably", "provides", "q", "que", "quite", "qv", "r", "rather", "rd", "re", "really", "reasonably", "regarding", "regardless", "regards", "relatively", "respectively", "right", "s", "said", "same", "saw", "say", "saying", "says", "second", "secondly", "see", "seeing", "seem", "seemed", "seeming", "seems", "seen", "self", "selves", "sensible", "sent", "serious", "seriously", "seven", "several", "shall", "she", "should", "shouldn't", "since", "six", "so", "some", "somebody", "somehow", "someone", "something", "sometime", "sometimes", "somewhat", "somewhere", "soon", "sorry", "specified", "specify", "specifying", "still", "sub", "such", "sup", "sure", "t", "t's", "take", "taken", "tell", "tends", "th", "than", "thank", "thanks", "thanx", "that", "that's", "thats", "the", "their", "theirs", "them", "themselves", "then", "thence", "there", "there's", "thereafter", "thereby", "therefore", "therein", "theres", "thereupon", "these", "they", "they'd", "they'll", "they're", "they've", "think", "third", "this", "thorough", "thoroughly", "those", "though", "three", "through", "throughout", "thru", "thus", "to", "together", "too", "took", "toward", "towards", "tried", "tries", "truly", "try", "trying", "twice", "two", "u", "un", "under", "unfortunately", "unless", "unlikely", "until", "unto", "up", "upon", "us", "use", "used", "useful", "uses", "using", "usually", "uucp", "v", "value", "various", "very", "via", "viz", "vs", "w", "want", "wants", "was", "wasn't", "way", "we", "we'd", "we'll", "we're", "we've", "welcome", "well", "went", "were", "weren't", "what", "what's", "whatever", "when", "whence", "whenever", "where", "where's", "whereafter", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while", "whither", "who", "who's", "whoever", "whole", "whom", "whose", "why", "will", "willing", "wish", "with", "within", "without", "won't", "wonder", "would", "would", "wouldn't", "x", "y", "yes", "yet", "you", "you'd", "you'll", "you're", "you've", "your", "yours", "yourself", "yourselves", "z","zero","january","february","march","april","may","june","july","august","september","october","november","december","a","be","www","and","the"
	#endregion
                             };


    /// <summary>
    /// htmll ascii chars according to SO 10646, ISO 8879, ISO 8859-1 Latin alphabet No. 1
    /// to clean input
    /// </summary>
    string[] htmlAsciiChars = { 
                    #region ascciChars
		                                      "&quot;", "&amp;", "&frasl;", "&lt;", "&gt;", "&euro;", "&dagger;", "&Dagger;", "&permil;", "&lsquo;", "&rsquo;", "&ldquo;", "&rdquo;", "&bull;", "&ndash;", "&mdash;", "&nbsp;", "&iexcl;", "&cent;", "&pound;", "&curren;", "&yen;", "&brvbar;", "&brkbar;", "&sect;", "&uml;", "&die;", "&copy;", "&ordf;", "&laquo;", "&not;", "&shy;", "&reg;", "&macr;", "&hibar;", "&deg;", "&plusmn;", "&sup2;", "&sup3;", "&acute;", "&micro;", "&para;", "&middot;", "&cedil;", "&sup1;", "&ordm;", "&raquo;", "&frac14;", "&frac12;", "&frac34;", "&iquest;", "&Agrave;", "&Aacute;", "&Acirc;", "&Atilde;", "&Auml;", "&Aring;", "&Aelig;", "&Ccedil;", "&Egrave;", "&Eacute;", "&Ecicr;", "&Euml;", "&Igrave;", "&Iacute;", "&Icirc;", "&Iuml;", "&ETH;", "&Dstrok;", "&Ntilde;", "&Ograve;", "&Oacute;", "&Ocirc;", "&Otilde;", "&Ouml;", "&times;", "&Oslash;", "&Ugrave;", "&Uacute;", "&Ucirc;", "&Uuml;", "&Yacute;", "&THORN;", "&szlig;", "&agrave;", "&aacute;", "&acirc;", "&atilde;", "&auml;", "&aring;", "&aelig;", "&ccedil;", "&egrave;", "&eacute;", "&ecirc;", "&euml;", "&igrave;", "&iacute;", "&icirc;", "&iuml;", "&eth;", "&ntilde", "&ograve;", "&oacute;", "&ocirc;", "&otilde;", "&ouml;", "&divide;", "&oslash;", "&ugrave;", "&uacute;", "&ucirc;", "&uuml;", "&yacute;", "&thorn;", "&yuml;" ,"•" ,"&#32;","&#33;","&#34;","&#35;","&#36;","&#37;","&#38;","&#39;","&#40;","&#41;","&#42;","&#43;","&#44;","&#45;","&#46;","&#47;","&#48;","&#49;","&#50;","&#51;","&#52;","&#53;","&#54;","&#55;","&#56;","&#57;","&#58;","&#59;","&#60;","&#61;","&#62;","&#63;","&#64;","&#65;","&#66;","&#67;","&#68;","&#69;","&#70;","&#71;","&#72;","&#73;","&#74;","&#75;","&#76;","&#77;","&#78;","&#79;","&#80;","&#81;","&#82;","&#83;","&#84;","&#85;","&#86;","&#87;","&#88;","&#89;","&#90;","&#91;","&#92;","&#93;","&#94;","&#95;","&#96;","&#97;","&#98;","&#99;","&#100;","&#101;","&#102;","&#103;","&#104;","&#105;","&#106;","&#107;","&#108;","&#109;","&#110;","&#111;","&#112;","&#113;","&#114;","&#115;","&#116;","&#117;","&#118;","&#119;","&#120;","&#121;","&#122;","&#123;","&#124;","&#125;","&#126;","&#127;"," ","-","/","\\"
	                    #endregion
                                  };

    public Suburb locationInformation { get; private set; }

    /// <summary>
    /// Preposition list for location detection
    /// </summary>
    List<string> prepositions = new List<string>() { 
           // "in", "at", "location", "locations", "area" ,"view","map","city","suburb","suburbs","place","places","australia"
            "places","australian","australia"
        };





    public List<string> PhoneNumbers { get; private set; }

    /// <summary>
    /// normalizes the content by
    /// removing the top words from it
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public string normalizeTheContent()
    {
      string content = this.Text2BNormalized;
      content = content.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");//.Replace(",", " ");
      content = stripAscii(content);
      content = cleanPrepositions(content);
      content = StripPhoneNumbers(content);
      content = new string(content.Where(c => !char.IsPunctuation(c)).ToArray()); //clears noktolama isaretleri
      string normalisedText = string.Empty;
      string[] textContent = content.ToLower().Split(' ');
      List<string> returVal = new List<string>();
      for (int i = 0; i < textContent.Length; i++)
      {
        if (!Array.Exists(stopWords, t => t.ToLower() == textContent[i].ToLower()))
        {
          if (textContent[i] != " ")
          {
            returVal.Add(textContent[i]);
          }
        }
      }
      /*
       * remove multiple space between the words
       * and add a single space instead
       */
      normalisedText = string.Join(" ", returVal.ToArray());
      this.Text2BNormalized = normalisedText;
      //call the function
      return normalisedText;
    }





    public TextEngine(string strTextContent)
    {
      this.Text2BNormalized = strTextContent;
      this.PhoneNumbers = new List<string>();
      normalizeTheContent();

    }


    /// <summary>
    /// strips ascii codes from the text and
    /// replace the character with a spaces
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private string stripAscii(string input)
    {
      for (int i = 0; i < htmlAsciiChars.Length; i++)
      {
        input = input.Replace(htmlAsciiChars[i], " ");
      }
      return input;
    }


    /// <summary>
    /// cleans the prepositions from the
    /// golden data a.k.a. identified ngrams list
    /// </summary>
    /// <param name="textElement"></param>
    private string cleanPrepositions(string textElement)
    {
      string retValue = textElement;
      foreach (string preposition in prepositions)
      {
        Regex.Split(textElement, @"\W").Any(
                wt =>
                {
                  if (wt.ToLower() == preposition.ToLower())
                  {
                    retValue = textElement.Replace(wt, " ");
                    return true;
                  }
                  else
                  {
                    return false;
                  }
                });
      }
      return retValue;
    }


    /// <summary>
    /// strip off the phone numbers from the text
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    private string StripPhoneNumbers(string strText)
    {
      string strRet = strText;
      string regexp = @"\\d{10,12}|\d{10,12}|\+\d{11}|\d{4}(\s+|)\d{2}(\s+|)\d{2}(\s+|)\d{2}|\d{2}(\s+|)\d{1,3}(\s+|)\d{3}(\s+|)\d{3}|\d{1,2}(\s+|)\d{1,2}(\s+|)\d{4}(\s+|)\d{4}|\d{2}(\s+|)\d{4}(\s+|)\d{4}|\x28\d{2}\x29((\s+|)+|)\d{4}(\s+|)\d{4}|\d{4}(\s+|)\d{4}";

      if (Regex.IsMatch(strText, regexp))
      {
        MatchCollection _matchCollection = Regex.Matches(strText, regexp);
        for (int i = 0; i < _matchCollection.Count; i++)
        {
          Match match = _matchCollection[i];
          strRet = strRet.Replace(match.Value, " ");
          if (!this.PhoneNumbers.Contains(match.Value))
            this.PhoneNumbers.Add(match.Value);
        }
      }
      return strRet;
    }


    /// <summary>
    /// allocate score for each ngram element
    /// based on their hit on australian states table
    /// </summary>
    /// <param name="Ngrams"></param>
    /// <param name="score"></param>
    /// <param name="AustLocatins"></param>
    public void AllocateScores(List<string> Ngrams, ref Dictionary<string, int> score, Locations AustLocatins)
    {
      foreach (var item in Ngrams)
      {
        Suburb[] result = Array.FindAll(AustLocatins.Location, s => s.Locality.ToLower().Equals(item.ToLower(), StringComparison.OrdinalIgnoreCase));
        if (result != null)
        {
          if (result.Length > 0)
          {
            if (score.ContainsKey(item))
            {
              int newscore;
              if (score.TryGetValue(item, out newscore))
              {
                newscore += 1;
                score[item] = newscore;
              }
            }
            else
            {
              score.Add(item, 1);
            }
          }
        }
      }
    }


    /// <summary>
    /// Creates N grams for address and any other
    /// pattern recognition
    /// </summary>
    /// <param name="normalizedText"></param>
    /// <param name="N"></param>
    /// <returns></returns>
    private List<string> getBiGrams()
    {
      string normalizedText = this.Text2BNormalized;
      normalizedText = Regex.Replace(normalizedText.Trim(), @"\s{2,}", " ");
      string[] textElements = normalizedText.ToLower().Split(' ');
      string[] refinedBody = Array.FindAll(textElements, t => t != "");
      string Ngram = string.Empty;
      UInt16 N = 1;
      List<string> NCollections = new List<string>();
      for (int i = 0; i < refinedBody.Length; i++)
      {
        int secCounter = i;
        int scope = secCounter + N;
        if (secCounter + N > refinedBody.Length)
        {
          scope = refinedBody.Length - 1;
        }
        for (int subCnt = secCounter; subCnt < scope; subCnt++)
        {
          Ngram = string.Concat(Ngram, " ", textElements[subCnt]);
        }
        if (Ngram != "")
        {
          NCollections.Add(Ngram.Trim());
        }
        Ngram = string.Empty;
      }
      return NCollections;
    }


    public List<NGramElement> getNGrams(UInt16 N = 1)
    {
      string normalizedText = this.Text2BNormalized;
      List<string> baseGram = this.getBiGrams(); //use this to allocate scores
      normalizedText = Regex.Replace(normalizedText.Trim(), @"\s{2,}", " ");
      string[] textElements = normalizedText.ToLower().Split(' ');
      string[] refinedBody = Array.FindAll(textElements, t => t != "");
      string Ngram = string.Empty;
      List<NGramElement> NCollections = new List<NGramElement>();
      //if bigram than dont do magic
      if (N == 1)
      {
        for (int i = 0; i < baseGram.Count; i++)
        {
          NCollections.Add(new NGramElement(baseGram[i], i + 1));
        }
      }
      else
      {
       
        for (int i = 0; i < refinedBody.Length; i++)
        {
          int secCounter = i;
          int scope = secCounter + N;
          List<int> wordPos = new List<int>();//this will store word locations
          List<int> pos = new List<int>();//this will store word locations based on bigrams
          if (secCounter + N > refinedBody.Length)
          {
            scope = refinedBody.Length - 1;
          }
          for (int subCnt = secCounter; subCnt < scope; subCnt++)
          {
            Ngram = string.Concat(Ngram, " ", textElements[subCnt]);
            wordPos.Add(subCnt);
            pos.Add(subCnt + 1);
          }
          if (Ngram != "")
          {
            NCollections.Add(new NGramElement(Ngram.Trim(),pos));   //(Ngram.Trim());
          }
          Ngram = string.Empty;
        }
      }
      return NCollections;
    }
  }



  public class NGramCollection
  {
    public  List<NGramElement> NGrams { get;  private set; }

    public NGramType NgramSize { get; private set; }

    public NGramCollection(List<NGramElement> NGram)
    {
      this.NGrams = NGram;
      this.NgramSize = NGrams[0].NgramSize;
    }

    /// <summary>
    /// if givem keword exists in the collectoin
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool Contains(string key)
    {
      return this.NGrams.Where(t => t.Keyword == key.ToLower()).Count() > 0;
    }
  }

  /// <summary>
  /// contains all the ngrams
  /// </summary>
  public class NgramCollectionContainer
  {
    public List<NGramCollection> Container { get; set; }
    public NgramCollectionContainer(params NGramCollection[] Ncollections)
    {
      this.Container = new List<NGramCollection>();
      for (int i = 0; i < Ncollections.Length; i++)
      {
        this.Container.Add(Ncollections[i]);
      }
    }

    public NGramCollection getCollection(NGramType NgramSize)
    {
      if (this.Container.Exists(t => t.NgramSize == NgramSize))
      {
        return this.Container.Where(x => x.NgramSize == NgramSize).First() as NGramCollection;
      }
      else
      {
        return null;
      }
    
    }
  }

    /// <summary>
    /// This object will help us to store Ngrams keys and their respective positions 
    /// which will be used for rating..
    /// </summary>
    public class NGramElement
    {
      /// <summary>
      /// Contains NGram keys
      /// </summary>
      public string Keyword { get; private set; }

      public NGramType NgramSize { get; private set; }


      /// <summary>
      /// Contains position of each key based on where N=1
      /// </summary>
      public List<KeyPosObject> KeyPosition { get; private set; }

      /// <summary>
      /// allocate indices to detect lines
      /// </summary>
      /// <param name="BiGramCollection"></param>
      public  NGramElement(string strKey,List<int> WordsPos)
      {
        this.Keyword = strKey;
        this.KeyPosition = new List<KeyPosObject>();
        string[] elems = this.Keyword.Split(' ');
        int size = elems.Length;
        this.NgramSize = (NGramType)size;
        for (int i = 0; i < elems.Length; i++)
        {
          this.KeyPosition.Add(new KeyPosObject() {Keyword=elems[i],KeywordPos=WordsPos[i] });
        }
      }

      public NGramElement(string strKey,int i)
      {
        this.Keyword = strKey;
        this.KeyPosition = new List<KeyPosObject>();
        this.KeyPosition.Add(new KeyPosObject() { Keyword = strKey, KeywordPos = i });
        int size = this.Keyword.Length;
        this.NgramSize = (NGramType)this.Keyword.Split(' ').Length;
      }

    }
    /// <summary>
    /// corrresponds size of the Ngram
    /// </summary>
    public enum NGramType
    {
      N1 = 1, N2 = 2, N3 = 3, N4 = 4, N5 = 5, N6 = 6, gr_other = 7
    }

  /// <summary>
  /// Each Ngram will have this struct to provide keyword-lineObject pair
  /// </summary>
  public struct KeyPosObject
  {
    //single word jkeyword of Ngram
    public string Keyword { get; set; }
    //position of the single word within givin text
    public int KeywordPos { get; set; }
  }

  /// <summary>
  /// This is for  mathed  localities contains all the matched elements for and scores
  /// </summary>
  public class HitListElement
  {

    public bool ToBeDeleted { get; set; }
    public int Score { get; set; }
    public NGramElement NgElement { get; private set; }
    public Suburb Locality { get; private set; }
    /// <summary>
    /// State indicator
    /// </summary>
    public bool isState { get; private set; }
    public HitListElement(NGramElement t,Suburb y)
    {
      this.NgElement = t;
      this.Locality = y;
      this.isState = y.PostCode != 0 ? false : true;
    }
    public HitListElement(HitListElement itm)
    {
      this.NgElement = itm.NgElement;
      this.Locality = itm.Locality;
      this.isState = itm.isState;
    }

    public HitListElement(Suburb itm)
    {
      this.isState = false;
      this.Locality = itm;
    }
  }


  public class HitListCollection
  {
    public List<HitListElement> Collection { get; private set; }
    public HitListCollection()
    {
      this.Collection = new List<HitListElement>();
    }

    public void Add(HitListElement t)
    {
      int ret = Contains(t);
      if (ret == 1)
      {
        this.Collection.Add(t);
      }
      else if (ret == 2)
      {
        t = Merge(t);
      }
      ScoreUp(t);
      //  DistanceCalculation();
    }

    private HitListElement Merge(HitListElement t)
    {
      HitListElement ret = null;
      if (this.Collection.Exists(x => x.NgElement.Keyword == t.NgElement.Keyword))
      {
        if (this.Collection.Exists(x => x.Locality == t.Locality))
        {
          ret = this.Collection.Where(x => x.NgElement.Keyword == t.NgElement.Keyword && x.Locality == t.Locality).FirstOrDefault();
          ret.NgElement.KeyPosition.AddRange(t.NgElement.KeyPosition);
          ret.NgElement.KeyPosition.OrderBy(g => g.KeywordPos);
        }
      }
      return ret;
    }

    /// <summary>
    /// return 
    /// 1 - ok add
    /// 2- same locality copy position information
    /// 3-everything is the same score up only
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public int Contains(HitListElement t)
    {
      int ret = 1;

      //return this.Collection.Exists(x => 
      //  x.NgElement.Keyword == t.NgElement.Keyword && x.NgElement.KeyPosition.Equals(t.NgElement.KeyPosition)
      //  &&
      //  x.Locality.State==t.Locality.State
      //  );

      if (this.Collection.Exists(x => x.NgElement.Keyword == t.NgElement.Keyword))
      {
        ret = 1; //
        if (this.Collection.Exists(x => x.Locality == t.Locality))
        {
          ret = 2;  //only copy positions
        }
      }
      else
      {
        ret = 1; //add the item to the collection everthing is ok!

      }
      return ret;
    }

    /// <summary>
    /// scores:
    /// 1- single word matches
    /// 3- Ngram matches where N>1
    /// 5-  terms side by side where have got the same state   via distance caculation
    /// </summary>
    /// <param name="t"></param>
    private void ScoreUp(HitListElement t, int Score = 0)
    {
      HitListElement element = this.Collection.Where(x =>
        x.NgElement.Keyword == t.NgElement.Keyword && x.NgElement.KeyPosition.Equals(t.NgElement.KeyPosition)
                &&
        x.Locality.State == t.Locality.State
        ).FirstOrDefault();
      int score = element.NgElement.NgramSize != NGramType.N1 ? 3 : 1;
      element.Score += score + Score;
    }
    /*
     * analyze the nearest location for extra scoring
     */


    public void DistanceCalculation()
    {
      List<int> itemToBeDeleted = new List<int>();

      for (int i = 0; i < this.Collection.Count; i++)
      {
        var item = this.Collection[i];
        if (!item.isState)
        {
          int Leftpos = item.NgElement.KeyPosition.Min(t => t.KeywordPos);
          //check if there is any matches which contaiuns leftpos-1
          var matches = Collection.Select(x => x.NgElement);
          foreach (var z in this.Collection)
          {
            if (z.NgElement.KeyPosition.Exists(h => h.KeywordPos == Leftpos - 1))
            {
              //check states
              var match = z.NgElement.KeyPosition.Where(h => h.KeywordPos == Leftpos - 1).First();
              if (z.Locality.State == item.Locality.State)
              {
                ScoreUp(item, 5);   //+5 addresses side by side for example [auburn nsw]
                ScoreUp(z, 5);
              }

            }
          }
        }
        else
        {
         
          //item is state transfer the score to those what has got the same state
          if (this.Collection.Exists(t => t.isState == false && t.Locality.State == item.Locality.State))
          {
            var ngrams = this.Collection.Where(t => t.isState == false && t.Locality.State == item.Locality.State);
            foreach (var listelem in ngrams)
            {
              listelem.Score += item.Score;
            }
            //if localities exist withing the same state then set the state type to be deleted otherwise keep them
            this.Collection[i].ToBeDeleted = true;
          }

        }
      }
      //delete state only eleemnts from the hitlist
      //for (int cntDel = 0; cntDel < itemToBeDeleted.Count; cntDel++)
      //{
      //  this.Collection.RemoveAt(itemToBeDeleted[cntDel]);
      //}
      this.Collection.RemoveAll(t => t.ToBeDeleted != false);
      //group the same localities in the collection table no need to have the same element  twise
      //add up the socores no need to worry about -		NgElement - since locality corresponds exact location ifnromation
      Dictionary<Suburb, List<int>> toBeMerged = new Dictionary<Suburb, List<int>>();
      for (int delIter = 0; delIter < this.Collection.Count; delIter++)
      {
        var iter = this.Collection[delIter];
        if (!toBeMerged.ContainsKey(iter.Locality))
        {
          toBeMerged.Add(iter.Locality, new List<int>() { delIter });
        }
        else
        {
          toBeMerged[iter.Locality].Add(delIter);
        }
      }
      var mergers = toBeMerged.Where(t => t.Value.Count > 1);
      int _score = 0;
      foreach (var _element in mergers)
      {
        foreach (var _item in _element.Value)
        {
          this.Collection[_item].ToBeDeleted = true;
          _score += this.Collection[_item].Score;
        }
        this.Collection.Add(new HitListElement(_element.Key) { Score = _score });
        _score = 0;
      }
      this.Collection.RemoveAll(t => t.ToBeDeleted != false);
    }
  }




}
