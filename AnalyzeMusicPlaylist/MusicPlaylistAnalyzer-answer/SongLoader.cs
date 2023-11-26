using System;
using System.Collections.Generic;
using System.IO;

namespace MusicPlaylistAnalyzer
{
    public static class SongLoader
    {
        private static int NumItemsInRow = 8;

        public static List<Song> Load(string songFilePath) {
            List<Song> songList = new List<Song>();

            try
            {
                using (var playlistreader = new StreamReader(songFilePath))
                {
                    int lineNumber = 0;
                    while (!playlistreader.EndOfStream)
                    {
                        var line = playlistreader.ReadLine();
                        lineNumber++;
                        if (lineNumber == 1) continue;

                        var values = line.Split('\t');

                        if (values.Length != NumItemsInRow)
                        {
                            throw new Exception($"Row {lineNumber} contains {values.Length} values. It should contain {NumItemsInRow}.");
                        }
                        try
                        {
                            string title = values[0];
                            string artist = values[1];
                            string album = values[2];
                            string genre = values[3];
                            int size = Int32.Parse(values[4]);
                            int time = Int32.Parse(values[5]);
                            int year = Int32.Parse(values[6]);
                            int plays = Int32.Parse(values[7]);
                            
                            Song song = new Song(title, artist, album, genre, size, time, year, plays);
                            songList.Add(song);

                            if(values.Length != 8)
                            {
                                Console.WriteLine("There should be eight values in the record.\nTitle, artist, album, genre, size, time, year and plays.");
                            }
                        }
                        catch (FormatException e)
                        {
                            throw new Exception($"Row {lineNumber} contains invalid data. {e.Message}");
                        }
                    }
                }
            } catch (Exception e){
                throw new Exception($"Unable to open {songFilePath} {e.Message}\nNOTE: This program runs under the assumption\nthat the playlist file is in the current directory.");
            }

            return songList;
        }
    }
}