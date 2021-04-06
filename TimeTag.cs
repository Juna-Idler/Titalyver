using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace LyricsTimeTag
{
	class TimeTag
	{
		public static int MinTime { get { return 0; } }
		public static string MinTimeTag
		{
			get { return "[00:00" + DecimalPoint + "00]"; }
		}

		public static int MaxTime { get { return (99 * 60 + 59) * 1000 + 99 * 10; } }
		public static string MaxTimeTag
		{
			get { return "[99:59" + DecimalPoint + "99]"; }
		}

		public static void SetDecimalPointPeriod() { decimalPoint = '.'; }
		public static void SetDecimalPointColon() { decimalPoint = ':'; }

		private static char decimalPoint = '.';
		public static char DecimalPoint { get { return decimalPoint; } }

		private static Regex allTagRegex = new Regex( @"\[.*?\]" );
		public static Regex AllTagRegex { get { return allTagRegex; } }

		private static Regex timeTagRegex = new Regex( @"\[\d\d:\d\d[:.]\d\d\]" );
		private static Regex headTimeTagRegex = new Regex( @"^\[\d\d:\d\d([:.]\d\d)?\]" );

		public static Regex TimeTagRegex { get { return timeTagRegex; } }
		public static Regex HeadTimeTagRegex { get { return headTimeTagRegex; } }

		public static int headtimetag2milisec( string timetag )
		{
			Match m = HeadTimeTagRegex.Match( timetag );
			if ( !m.Success )
				return -1;

			int minute = int.Parse( timetag.Substring( 1 , 2 ) );
			int second = int.Parse( timetag.Substring( 4 , 2 ) );
			int mili10 = 0;
			if ( m.Length == 10 )
				mili10 = int.Parse( timetag.Substring( 7 , 2 ) );
			return (minute * 60 + second) * 1000 + mili10 * 10;
		}

		public static int timetag2milisec( string timetag )
		{
			if ( timetag.Length < 10 || timetag[0] != '[' || !char.IsDigit( timetag[1] ) )
				return -1;
			int minute = int.Parse( timetag.Substring( 1 , 2 ) );
			int second = int.Parse( timetag.Substring( 4 , 2 ) );
			int mili10 = int.Parse( timetag.Substring( 7 , 2 ) );

			return (minute * 60 + second) * 1000 + mili10 * 10;
		}
		public static string milisec2timetag( int milisec )
		{
			if ( milisec < 0 )
				return "";
			return string.Format( "[{0:D2}:{1:D2}" + DecimalPoint + "{2:D2}]" , milisec / 1000 / 60 , milisec / 1000 % 60 , milisec % 1000 / 10 );
		}

		public static string RemoveTag( string text )
		{
			return AllTagRegex.Replace( text , "" );
		}

		public static string RemoveTimeTag( string text )
		{
			return TimeTagRegex.Replace( text , "" );
		}


		public class Pair
		{
			public int milisec;
			public string tt
			{
				get
				{
					if (milisec >= 0)
						return milisec2timetag( milisec );
					return "";
				}
				set { milisec = timetag2milisec( value ); }
			}
			public string word;

			public Pair( int milisec , string word ) { this.milisec = milisec; this.word = word; }
			public Pair( string tt , string word ) { this.milisec = timetag2milisec( tt ); this.word = word; }
		}

		public static Pair SeparateHeadTimeTagLine( string line )
		{
			Match m = HeadTimeTagRegex.Match( line );
			if ( m.Success )
			{
				return new Pair( headtimetag2milisec( m.Value ) , line.Substring( m.Length ) );
			}
			return new Pair( -1 , line );
		}
		public static List<Pair> SeparateHeadTimeTag( string text )
		{
			StringReader sr = new StringReader( text );

			List<Pair> ret = new List<Pair>();
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				ret.Add( SeparateHeadTimeTagLine(line) );
			}
			return ret;
		}

		public static List<int> PickupHeadTimeTag( string text )
		{
			StringReader sr = new StringReader( text );
			List<int> ret = new List<int>();
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				ret.Add( headtimetag2milisec( line ) );
			}
			return ret;
		}

		public static List<int> PickupTimeTag( List<Pair> pairs )
		{
			List<int> ret = new List<int>();
			foreach ( Pair p in pairs )
			{
				ret.Add( p.milisec );
			}
			return ret;
		}

		public static List<Pair> SeparateKaraokeLine( string line )
		{
			int offset = 0;
			string tt = "";
			Match h = HeadTimeTagRegex.Match( line );
			if ( h.Success )
			{
				tt = h.Value;
				offset = h.Length;
			}
			List<Pair> ret = new List<Pair>();
			MatchCollection mc = TimeTag.TimeTagRegex.Matches( line , offset );
			if ( mc.Count == 0 )
			{
				ret.Add( new Pair( tt , line.Substring( offset ) ) );
				return ret;
			}

			ret.Add( new Pair( tt , line.Substring( offset , mc[0].Index - offset ) ) );

			int i;
			for ( i = 1 ; i < mc.Count ; i++ )
			{
				offset = mc[i - 1].Index + mc[i - 1].Length;
				ret.Add( new Pair( mc[i - 1].Value , line.Substring( offset , mc[i].Index - offset ) ) );
			}
			offset = mc[i - 1].Index + mc[i - 1].Length;
			ret.Add( new Pair( mc[i - 1].Value , line.Substring( offset ) ) );

			return ret;
		}

		public static List<List<Pair>> SeparateKaraokeText( string text )
		{
			StringReader sr = new StringReader( text );
			List<List<Pair>> ret = new List<List<Pair>>();
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				ret.Add( SeparateKaraokeLine( line ) );
			}
			return ret;
		}

		public static List<int> PickupKaraokeTagLine( string line )
		{
			List<int> ret = new List<int>();
			int offset = 0;
			int milisec = -1;
			Match h = HeadTimeTagRegex.Match( line );
			if ( h.Success )
			{
				milisec = headtimetag2milisec( h.Value );
				offset = h.Length;
			}
			ret.Add( milisec );
			MatchCollection mc = TimeTag.TimeTagRegex.Matches( line , offset );
			if ( mc.Count == 0 )
				return ret;

			foreach ( Match m in mc)
			{
				ret.Add( timetag2milisec( m.Value ) );
			}
			return ret;
		}
		public static List<List<int>> PickupKaraokeTag( string text )
		{
			StringReader sr = new StringReader( text );
			List<List<int>> ret = new List<List<int>>();
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				ret.Add( PickupKaraokeTagLine( line ) );
			}
			return ret;
		}

		public static List<List<int>> PickupKaraokeTag( List<List<Pair>> pairsline )
		{
			List<List<int>> ret = new List<List<int>>();
			foreach ( List<Pair> lp in pairsline )
			{
				List<int> a = new List<int>();
				foreach ( Pair p in lp )
				{
					a.Add( p.milisec );
				}
				ret.Add( a );
			}
			return ret;
		}


		public static string RemoveKaraokeTag( string text )
		{
			string ret = "";
			StringReader sr = new StringReader( text );
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				Match m = TimeTag.HeadTimeTagRegex.Match( line );
				if ( m.Success )
				{
					ret += m.Value + TimeTag.TimeTagRegex.Replace( line , "" ) + Environment.NewLine;
				}
				else
				{
					ret += line + Environment.NewLine;
				}
			}
			return ret;
		}

//		static private Regex diffTimeTagRegex = new Regex( @"\[\d+]" );
//		static public Regex DiffTimeTagRegex { get { return diffTimeTagRegex; } }
/*
		static public int difftag2milisec( string difftag )
		{
			if ( difftag[0] != '[' || !char.IsDigit( difftag[1] ) )
				return -1;
			int length = 0;
			foreach ( char c in difftag)
			{
				if (c == ']')
					break;
				if ( !char.IsDigit( c ) )
					return -1;
				length++;
			}
			return int.Parse(difftag.Substring(1,length));
		}
		static public string milisec2difftag( int milisec )
		{
			return string.Format( "[{0:D}]" , milisec / 10 );
		}
*/


/*
		static public Pair[] SeparateKaraokeTextDiffTag( string text )
		{
			int offset = 0;
			string tt = "";
			Match m = HeadTimeTagRegex.Match( text );
			if ( m.Success )
			{
				tt = m.Value;
				offset = m.Length;
			}
			MatchCollection mc = TimeTag.DiffTimeTagRegex.Matches( text );
			if ( mc.Count == 0 )
			{
				return new Pair[1] { new Pair( tt , text.Substring( offset ) ) };
			}

			Pair[] pairs = new Pair[mc.Count + 1];
			pairs[0] = new Pair( tt , text.Substring( offset , mc[0].Index - offset ) );

			int i;
			for (i = 1 ; i < mc.Count ; i++ )
			{
				offset = mc[i-1].Index + mc[i-1].Length;
				pairs[i] = new Pair( mc[i-1].Value , text.Substring( offset , mc[i].Index - offset ) );
			}
			offset = mc[i-1].Index + mc[i-1].Length;
			pairs[i] = new Pair( mc[i-1].Value , text.Substring( offset ) );

			return pairs;
		}
*/

/*
		static public string ConvertFullToDiff( string text )
		{
			StringReader sr = new StringReader( text );
			string ret = "";
			int lastlinetime = 0;
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				int lasttime;
				Pair[] pairs = SeparateKaraokeTextFullTag( line );
				ret += pairs[0].tt + pairs[0].word;
				if ( pairs[0].tt == "" )
				{
					lasttime = lastlinetime;
				}
				else
				{
					lasttime = timetag2milisec( pairs[0].tt );
					lastlinetime = lasttime;
				}
				for ( int i = 1 ; i < pairs.Length ; i++ )
				{
					int currenttime = timetag2milisec( pairs[i].tt );
					int diff = currenttime - lasttime;
					ret += milisec2difftag(diff) + pairs[i].word;
					lasttime = currenttime;
					lastlinetime = currenttime;
				}
				ret += Environment.NewLine;
			}
			return ret;
		}

		static public string ConvertDiffToFull( string text )
		{
			StringReader sr = new StringReader( text );
			string ret = "";
			int lastlinetime = 0;
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				int lasttime;
				Pair[] pairs = SeparateKaraokeTextDiffTag( line );
				ret += pairs[0].tt + pairs[0].word;
				if ( pairs[0].tt == "" )
				{
					lasttime = lastlinetime;
				}
				else
				{
					lasttime = timetag2milisec( pairs[0].tt );
					lastlinetime = lasttime;
				}
				for ( int i = 1 ; i < pairs.Length ; i++ )
				{
					int currenttime = lasttime + difftag2milisec( pairs[i].tt );
					ret += milisec2timetag(currenttime) + pairs[i].word;
					lasttime = currenttime;
					lastlinetime = currenttime;
				}
				ret += Environment.NewLine;
			}
			return ret;
		}
 */ 
	}

	class AtTag
	{
		public class Pair
		{
			public string name;
			public string value;
		}

		public static List<Pair> LoadAtTags( string text )
		{
			StringReader sr = new StringReader( text );
			List<Pair> ret = new List<Pair>();
			string line;
			while ( (line = sr.ReadLine()) != null )
			{
				if ( line != "" && line[0] == '@' )
				{
					Pair p = SeparateAtTag( line );
					if ( p != null )
						ret.Add( p );
				}
			}
			return ret;
		}

		public static Pair SeparateAtTag( string line )
		{
			if ( line == "" || line[0] != '@' )
				return null;
			int equal = line.IndexOf( '=' );
			if ( equal == -1 )
				return null;
			Pair ret = new Pair();
			ret.name = line.Substring( 1 , equal - 1 ).TrimEnd( null );
			ret.value = line.Substring( equal + 1 );
			return ret;
		}

	}
}
