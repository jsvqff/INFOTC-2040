using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Song
{
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Genre { get; set; }
    public int Size { get; set; }
    public int Time { get; set; }
    public int Year { get; set; }
    public int Plays { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Artist: {Artist}, Album: {Album}, Genre: {Genre}, Size: {Size}, Time: {Time}, Year: {Year}, Plays: {Plays}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: AnalyzeMusicPlaylist <music_playlist_file_path> <report_file_path>");
            return;
        }

        string dataFilePath = args[0];
        string reportFilePath = args[1];

        List<Song> songs = new List<Song>();

        try
        {
            using (StreamReader reader = new StreamReader(dataFilePath))
            {
                // Skip the header line
                reader.ReadLine();

                string line;
                int lineNumber = 1;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] values = line.Split('\t');

                    if (values.Length == 8)
                    {
                        Song song = new Song
                        {
                            Name = values[0],
                            Artist = values[1],
                            Album = values[2],
                            Genre = values[3],
                            Size = int.Parse(values[4]),
                            Time = int.Parse(values[5]),
                            Year = int.Parse(values[6]),
                            Plays = int.Parse(values[7])
                        };

                        songs.Add(song);
                    }
                    else
                    {
                        Console.WriteLine($"Row {lineNumber} contains {values.Length} values. It should contain 8.");
                    }

                    lineNumber++;
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: Playlist data file not found.");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return;
        }

        StringBuilder report = new StringBuilder();

        report.AppendLine("Music Playlist Report");
        report.AppendLine();

        // Question 1: How many songs received 200 or more plays?
        var songsWith200OrMorePlays = songs.Where(song => song.Plays >= 200);
        report.AppendLine("Songs that received 200 or more plays:");
        foreach (var song in songsWith200OrMorePlays)
        {
            report.AppendLine(song.ToString());
        }
        report.AppendLine();

        // Question 2: How many songs are in the playlist with the Genre of "Alternative"?
        var alternativeSongsCount = songs.Count(song => song.Genre == "Alternative");
        report.AppendLine($"Number of Alternative songs: {alternativeSongsCount}");
        report.AppendLine();

        // Question 3: How many songs are in the playlist with the Genre of "Hip-Hop/Rap"?
        var hipHopRapSongsCount = songs.Count(song => song.Genre == "Hip-Hop/Rap");
        report.AppendLine($"Number of Hip-Hop/Rap songs: {hipHopRapSongsCount}");
        report.AppendLine();

        // Question 4: What songs are in the playlist from the album "Welcome to the Fishbowl?"
        var welcomeToTheFishbowlSongs = songs.Where(song => song.Album == "Welcome to the Fishbowl");
        report.AppendLine("Songs from the album Welcome to the Fishbowl:");
        foreach (var song in welcomeToTheFishbowlSongs)
        {
            report.AppendLine(song.ToString());
        }
        report.AppendLine();

        // Question 5: What are the songs in the playlist from before 1970?
        var songsBefore1970 = songs.Where(song => song.Year < 1970);
        report.AppendLine("Songs from before 1970:");
        foreach (var song in songsBefore1970)
        {
            report.AppendLine(song.ToString());
        }
        report.AppendLine();

        // Question 6: What are the song names that are more than 85 characters long?
        var longSongNames = songs.Where(song => song.Name.Length > 85).Select(song => song.Name);
        report.AppendLine("Song names longer than 85 characters:");
        foreach (var name in longSongNames)
        {
            report.AppendLine(name);
        }
        report.AppendLine();

        // Question 7: What is the longest song? (longest in Time)
        var longestSong = songs.OrderByDescending(song => song.Time).FirstOrDefault();
        report.AppendLine("Longest song:");
        report.AppendLine(longestSong.ToString());
        report.AppendLine();

        // Question 8: What are the unique Genres in the playlist?
        var uniqueGenres = songs.Select(song => song.Genre).Distinct();
        report.AppendLine("Unique Genres:");
        foreach (var genre in uniqueGenres)
        {
            report.AppendLine(genre);
        }
        report.AppendLine();

        // Question 9: How many songs were produced each year in the playlist?
        var songsPerYear = songs.GroupBy(song => song.Year)
                                .Select(group => new { Year = group.Key, Count = group.Count() })
                                .OrderByDescending(item => item.Year);
        report.AppendLine("Yearly Number of Songs in Playlist:");
        foreach (var item in songsPerYear)
        {
            report.AppendLine($"{item.Year}: {item.Count}");
        }
        report.AppendLine();

        // Question 10: What are the total plays per year in the playlist?
        var totalPlaysPerYear = songs.GroupBy(song => song.Year)
                                    .Select(group => new { Year = group.Key, TotalPlays = group.Sum(song => song.Plays) })
                                    .OrderByDescending(item => item.Year);
        report.AppendLine("Total Plays Per Year:");
        foreach (var item in totalPlaysPerYear)
        {
            report.AppendLine($"{item.Year}: {item.TotalPlays}");
        }

        File.WriteAllText(reportFilePath, report.ToString());

        Console.WriteLine("Report generated successfully.");
    }
}
