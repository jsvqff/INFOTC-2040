using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MusicPlaylistAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            string songFilePath;
            string reportFilePath;
            string response;
            int errorCount = 0;
            
            if(args.Length < 2)
            {
                Console.WriteLine("Not enough arguments. Type playlist file name: ");
                songFilePath = Console.ReadLine();
                Console.WriteLine("Enter report document name: ");
                reportFilePath = Console.ReadLine();
            }
            else
            {
                 songFilePath = args[0];
                 reportFilePath = args[1];
            }


            do
            {              
                Console.WriteLine("Do you want the files you provided to be .txt files? Y/N");
                response = Console.ReadLine().ToUpper();
                errorCount++;
                if(errorCount > 3)
                {
                    Console.WriteLine("Hey. Answer only, Y or N, BUDDY!");
                }
                if(response.ToUpper() == "Y" || response.ToUpper() == "N")
                    break;
            }while(response.ToUpper() != "Y" || response.ToUpper() != "N");
            
            if(response == "Y")
            {
                if(!songFilePath.Contains(".txt"))
                {
                    songFilePath += ".txt";
                }
                if(!reportFilePath.Contains(".txt"))
                {
                    reportFilePath += ".txt";
                }
            }
            

            //Making the report:
            List<Song> songList = null;
            try
            {
                songList = SongLoader.Load(songFilePath);

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Environment.Exit(2);
            }

            try
            {
                using (var reportwriter = new StreamWriter(reportFilePath))
                {


                    var songsLotsOfPlays = from song in songList where song.Plays >= 200 select song;
                    int count = 0;
                    reportwriter.WriteLine("*\\*/*\\*/*\\*/*\\*");
                    reportwriter.WriteLine("Songs with 200 or more plays:");
                    foreach(var selected in songsLotsOfPlays)
                    {
                        reportwriter.WriteLine("Title: " + selected.Title + " |" + " Arist: " + selected.Artist + " |" + " Album: " + selected.Album + " |" + 
                        " Genre: " + selected.Genre + " |" + " Time: " + selected.Time + " |" + " Size: " + selected.Size + " |" + " Year: " + selected.Year + " |" + " Plays: "+ selected.Plays);
                     reportwriter.WriteLine("---------");
                        count++;
                    }
                    reportwriter.WriteLine("There were " + count + " songs.");

                    reportwriter.WriteLine();
                    reportwriter.WriteLine("*\\*/*\\*/*\\*/*\\*");
                    count = 0;
                    var alternativeSongs = from song in songList where song.Genre == "Alternative" select song;
                    foreach(var selected in alternativeSongs)
                    {
                    count++;
                    }
                    reportwriter.WriteLine("Number of Alternative songs: " + count);
                    
                    reportwriter.WriteLine();
                    reportwriter.WriteLine("*\\*/*\\*/*\\*/*\\*");
                    count = 0;
                    var hiphoprapSongs = from song in songList where song.Genre == "Hip-Hop/Rap" select song;
                    foreach(var selected in hiphoprapSongs)
                    {
                     count++;
                 }
                 reportwriter.WriteLine("Number of Hip-Hop/Rap songs: " + count);

                    reportwriter.WriteLine();
                    reportwriter.WriteLine("*\\*/*\\*/*\\*/*\\*");
                    reportwriter.WriteLine("Songs also appearing in \"Welcome to the Fishbowl\":");
                    var fishbowlSongs = from song in songList where song.Album == "Welcome to the Fishbowl" select song;
                    foreach(var selected in fishbowlSongs)
                    {
                     reportwriter.WriteLine("Title: " + selected.Title + " |" + " Arist: " + selected.Artist + " |" + " Album: " + selected.Album + " |" + 
                        " Genre: " + selected.Genre + " |" + " Time: " + selected.Time + " |" + " Size: " + selected.Size + " |" + " Year: " + selected.Year + " |" + " Plays: "+ selected.Plays);
                        reportwriter.WriteLine("---------");
                    }

                    reportwriter.WriteLine();
                    reportwriter.WriteLine("*\\*/*\\*/*\\*/*\\*");
                    reportwriter.WriteLine("Song that were released before 1970:");
                    var oldies = from song in songList where song.Year < 1970 select song;
                    foreach(var selected in oldies)
                    {
                     reportwriter.WriteLine("Title: " + selected.Title + " |" + " Arist: " + selected.Artist + " |" + " Album: " + selected.Album + " |" + 
                        " Genre: " + selected.Genre + " |" + " Time: " + selected.Time + " |" + " Size: " + selected.Size + " |" + " Year: " + selected.Year + " |" + " Plays: "+ selected.Plays);
                        reportwriter.WriteLine("---------");
                    }

                    reportwriter.WriteLine();
                    reportwriter.WriteLine("*\\*/*\\*/*\\*/*\\*");
                    reportwriter.WriteLine("Song names longer than 85 characters: ");
                    var longNames = from song in songList where song.Title.Count() > 85 select song.Title;
                    foreach(var selected in longNames)
                    {
                        reportwriter.WriteLine(selected);
                        reportwriter.WriteLine("---------");
                    }

                    reportwriter.WriteLine();
                    reportwriter.WriteLine("*\\*/*\\*/*\\*/*\\*");
                    reportwriter.WriteLine("Song with the longest time: ");
                    var longestSong = (from song in songList where song.Time > 1 orderby song.Time descending select song).FirstOrDefault();
                    reportwriter.WriteLine(longestSong.Title);
                    reportwriter.WriteLine("*\\*/*\\*/*\\*/*\\*");
                    reportwriter.Close();

                
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
                //if report can't be created
            }
            finally
            {
                if(!File.Exists(reportFilePath))
                {
                    Console.WriteLine("Couldn't create that report file... Try naming it something else.");
                }
                else
                    Console.WriteLine("Playlist report was successfully created in the current directory!");
            }
          
        }
    }
}
