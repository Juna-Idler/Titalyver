using System;
using System.Collections.Generic;
using System.Text;

using System.IO;


namespace Juna
{
	class LyricsTextFile
	{
		public enum LyricsType
		{
			Null = 0 ,
			Lyrics = 1 ,
			Karaoke = 2 ,
			Text = 3
		}

		public enum FileType
		{
			Text = 0,
			ID3 = 1,

		}

		private LyricsType lyrics_type_ = LyricsType.Null;
		private FileType file_type_ = FileType.Text;
		private string filepath_ = "";
		private Encoding encoding_ = null;
		private string text_ = "";

		public LyricsType LType { get { return lyrics_type_; } }
		public FileType FType { get { return file_type_; } }
		public string FilePath { get { return filepath_; } }
		public Encoding Encoding { get { return encoding_; } }
		public string Text { get { return text_; } }

		public bool Open( string filepath )
		{
			Close();
			Encoding encoding = null;
			string ext = System.IO.Path.GetExtension( filepath ).ToLower();

			try
			{
				using ( FileStream fs = new FileStream( filepath , FileMode.Open , FileAccess.Read , FileShare.Read ) )
				{
					switch ( ext )
					{
					case ".mp3":
						SoundTag.ID3v2.ID3v2Tag v2 = new SoundTag.ID3v2.ID3v2Tag( fs );
						SoundTag.ID3v2.Frame f = v2.Frames.Find( delegate( SoundTag.ID3v2.Frame t ) { return t.FrameID == "SYLT"; } );
						if ( f != null )
						{
							SoundTag.ID3v2.Frames.SynchronisedLyrics sylt = new SoundTag.ID3v2.Frames.SynchronisedLyrics( f );
							if ( sylt.TimeStampFormat != SoundTag.ID3v2.Frames.TimeStampFormat.AbsoluteTime32_MilliSeconds )
								return false;
							StringBuilder sb = new StringBuilder();
							foreach ( SoundTag.ID3v2.Frames.SynchronisedLyrics.SyncBlock l in sylt.Lyrics )
							{
								sb.AppendFormat( "[{0:D2}:{1:D2}.{2:D2}]" + l.text + Environment.NewLine , l.time_stamp / 1000 / 60 , (l.time_stamp / 1000) % 60 , (l.time_stamp / 10) % 100 );
							}
							text_ = sb.ToString();
							filepath_ = filepath;
							encoding_ = SoundTag.ID3v2.Frame.GetTextEncoding( sylt.TextEncoding );
							lyrics_type_ = LyricsType.Lyrics;
							file_type_ = FileType.ID3;

							return true;
						}
						f = v2.Frames.Find( delegate( SoundTag.ID3v2.Frame t ) { return t.FrameID == "USLT"; } );
						if ( f != null )
						{
							SoundTag.ID3v2.Frames.UnsynchronisedLyrics uslt = new SoundTag.ID3v2.Frames.UnsynchronisedLyrics( f );
							text_ = uslt.Lyrics;
							filepath_ = filepath;
							encoding_ = SoundTag.ID3v2.Frame.GetTextEncoding( uslt.TextEncoding );
							lyrics_type_ = LyricsType.Text;
							file_type_ = FileType.ID3;
							return true;
						}
						return false;

					case ".lrc":
						lyrics_type_ = LyricsType.Text;
						break;
					case ".kra":
						lyrics_type_ = LyricsType.Text;
						break;
					case ".krr":
						lyrics_type_ = LyricsType.Text;
						break;
					case ".txt":
						lyrics_type_ = LyricsType.Text;
						break;
					default:
						return false;
					}
					//text file
					byte[] bom = new byte[3];
					int r = fs.Read( bom , 0 , 3 );
					if ( r < 2 )
						return false;
					if ( bom[0] == 0xFF && bom[1] == 0xFE )		//utf-16 LE
					{
						encoding = Encoding.Unicode;
						fs.Seek( 2 , SeekOrigin.Begin );
					}
					else if ( bom[0] == 0xFE && bom[1] == 0xFF )	//utf-16 BE
					{
						encoding = Encoding.BigEndianUnicode;
						fs.Seek( 2 , SeekOrigin.Begin );
					}
					else if ( bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF ) // utf-8
					{
						encoding = Encoding.UTF8;
						fs.Seek( 3 , SeekOrigin.Begin );
					}
					else	//shift-jisと言うことにする
					{
						encoding = Encoding.GetEncoding( "shift-jis" );
						fs.Seek( 0 , SeekOrigin.Begin );
					}
					using ( StreamReader sr = new StreamReader( fs , encoding ) )
					{
						text_ = sr.ReadToEnd();
					}
					file_type_ = FileType.Text;
					this.filepath_ = filepath;
					this.encoding_ = encoding;
					return true;
				}
			}
			catch ( Exception e )
			{
				return false;
			}

		}

		public void Close()
		{
			filepath_ = "";
			encoding_ = null;
			text_ = "";
		}
	}

	class CharcterCode
	{
		/// <summary>
		/// 文字コードを判別する
		/// </summary>
		/// <remarks>
		/// Jcode.pmのgetcodeメソッドを移植したものです。
		/// Jcode.pm(http://openlab.ring.gr.jp/Jcode/index-j.html)
		/// Jcode.pmのCopyright: Copyright 1999-2005 Dan Kogai
		/// </remarks>
		/// <param name="bytes">文字コードを調べるデータ</param>
		/// <returns>適当と思われるEncodingオブジェクト。
		/// 判断できなかった時はnull。</returns>
		public static System.Text.Encoding GetCode(byte[] bytes)
		{
			const byte bEscape = 0x1B;
			const byte bAt = 0x40;
			const byte bDollar = 0x24;
			const byte bAnd = 0x26;
			const byte bOpen = 0x28;    //'('
			const byte bB = 0x42;
			const byte bD = 0x44;
			const byte bJ = 0x4A;
			const byte bI = 0x49;

			int len = bytes.Length;
			byte b1, b2, b3, b4;

			//Encode::is_utf8 は無視

			bool isBinary = false;
			for (int i = 0; i < len; i++)
			{
				b1 = bytes[i];
				if (b1 <= 0x06 || b1 == 0x7F || b1 == 0xFF)
				{
					//'binary'
					isBinary = true;
					if (b1 == 0x00 && i < len - 1 && bytes[i + 1] <= 0x7F)
					{
						//smells like raw unicode
						return System.Text.Encoding.Unicode;
					}
				}
			}
			if (isBinary)
			{
				return null;
			}

			//not Japanese
			bool notJapanese = true;
			for (int i = 0; i < len; i++)
			{
				b1 = bytes[i];
				if (b1 == bEscape || 0x80 <= b1)
				{
					notJapanese = false;
					break;
				}
			}
			if (notJapanese)
			{
				return System.Text.Encoding.ASCII;
			}

			for (int i = 0; i < len - 2; i++)
			{
				b1 = bytes[i];
				b2 = bytes[i + 1];
				b3 = bytes[i + 2];

				if (b1 == bEscape)
				{
					if (b2 == bDollar && b3 == bAt)
					{
						//JIS_0208 1978
						//JIS
						return System.Text.Encoding.GetEncoding(50220);
					}
					else if (b2 == bDollar && b3 == bB)
					{
						//JIS_0208 1983
						//JIS
						return System.Text.Encoding.GetEncoding(50220);
					}
					else if (b2 == bOpen && (b3 == bB || b3 == bJ))
					{
						//JIS_ASC
						//JIS
						return System.Text.Encoding.GetEncoding(50220);
					}
					else if (b2 == bOpen && b3 == bI)
					{
						//JIS_KANA
						//JIS
						return System.Text.Encoding.GetEncoding(50220);
					}
					if (i < len - 3)
					{
						b4 = bytes[i + 3];
						if (b2 == bDollar && b3 == bOpen && b4 == bD)
						{
							//JIS_0212
							//JIS
							return System.Text.Encoding.GetEncoding(50220);
						}
						if (i < len - 5 &&
							b2 == bAnd && b3 == bAt && b4 == bEscape &&
							bytes[i + 4] == bDollar && bytes[i + 5] == bB)
						{
							//JIS_0208 1990
							//JIS
							return System.Text.Encoding.GetEncoding(50220);
						}
					}
				}
			}

			//should be euc|sjis|utf8
			//use of (?:) by Hiroki Ohzaki <ohzaki@iod.ricoh.co.jp>
			int sjis = 0;
			int euc = 0;
			int utf8 = 0;
			for (int i = 0; i < len - 1; i++)
			{
				b1 = bytes[i];
				b2 = bytes[i + 1];
				if (((0x81 <= b1 && b1 <= 0x9F) || (0xE0 <= b1 && b1 <= 0xFC)) &&
					((0x40 <= b2 && b2 <= 0x7E) || (0x80 <= b2 && b2 <= 0xFC)))
				{
					//SJIS_C
					sjis += 2;
					i++;
				}
			}
			for (int i = 0; i < len - 1; i++)
			{
				b1 = bytes[i];
				b2 = bytes[i + 1];
				if (((0xA1 <= b1 && b1 <= 0xFE) && (0xA1 <= b2 && b2 <= 0xFE)) ||
					(b1 == 0x8E && (0xA1 <= b2 && b2 <= 0xDF)))
				{
					//EUC_C
					//EUC_KANA
					euc += 2;
					i++;
				}
				else if (i < len - 2)
				{
					b3 = bytes[i + 2];
					if (b1 == 0x8F && (0xA1 <= b2 && b2 <= 0xFE) &&
						(0xA1 <= b3 && b3 <= 0xFE))
					{
						//EUC_0212
						euc += 3;
						i += 2;
					}
				}
			}
			for (int i = 0; i < len - 1; i++)
			{
				b1 = bytes[i];
				b2 = bytes[i + 1];
				if ((0xC0 <= b1 && b1 <= 0xDF) && (0x80 <= b2 && b2 <= 0xBF))
				{
					//UTF8
					utf8 += 2;
					i++;
				}
				else if (i < len - 2)
				{
					b3 = bytes[i + 2];
					if ((0xE0 <= b1 && b1 <= 0xEF) && (0x80 <= b2 && b2 <= 0xBF) &&
						(0x80 <= b3 && b3 <= 0xBF))
					{
						//UTF8
						utf8 += 3;
						i += 2;
					}
				}
			}
			//M. Takahashi's suggestion
			//utf8 += utf8 / 2;

			System.Diagnostics.Debug.WriteLine(
				string.Format("sjis = {0}, euc = {1}, utf8 = {2}", sjis, euc, utf8));
			if (euc > sjis && euc > utf8)
			{
				//EUC
				return System.Text.Encoding.GetEncoding(51932);
			}
			else if (sjis > euc && sjis > utf8)
			{
				//SJIS
				return System.Text.Encoding.GetEncoding(932);
			}
			else if (utf8 > euc && utf8 > sjis)
			{
				//UTF8
				return System.Text.Encoding.UTF8;
			}

			return null;
		}
	}
}



