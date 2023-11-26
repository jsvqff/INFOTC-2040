using System;
namespace MusicPlaylistAnalyzer
{
    public class Song
    {
        public string Title;
        public string Artist;
        public string Album;
        public string Genre;
        public int Size;
        public int Time;
        public int Year;
        public int Plays;

        public Song(string title, string artist, string album, string genre, int size, int time, int year, int plays)
        {
            Title = title;
            Artist = artist;
            Album = album;
            Genre = genre;
            Size = size;
            Time = time;
            Year = year;
            Plays = plays;

        }

    }
}