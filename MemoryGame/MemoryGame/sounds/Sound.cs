using System;
using System.Collections.Generic;
using System.IO;
using MemoryGame.Properties;
using NAudio.Wave;

namespace MemoryGame
{
    public static class Sound
    {
        public static void StartEffect(byte[] audioResource)
        {
            // Create WaveStream (inherits from System.IO.Stream), and read the MP3 file
            WaveStream mainOutputStream = new Mp3FileReader(ByteArrayToStream(audioResource));
            WaveChannel32 volumeStream = new WaveChannel32(mainOutputStream);

            // Create the player
            WaveOutEvent player = new WaveOutEvent();

            // Add the volumeStream to the player
            player.Init(volumeStream);

            // Start playback
            player.Play();
        }

        public static WaveOutEvent BackgroundPlayer = new WaveOutEvent();

        public static void StartBackgroundMusic(int selectedTheme)
        {
            // Create lists with songs per theme
            List<byte[]> animalsSongs = new List<byte[]>() { Resources.bangerbeat, Resources.angello };
            List<byte[]> lotrSongs = new List<byte[]>() { Resources.lotr_main, Resources.lotr_gondor };
            List<byte[]> starWarsSongs = new List<byte[]>() { Resources.star_wars_main, Resources.star_wars_imperial_march };

            // Create an empty Stream
            Stream audioStream = new MemoryStream();

            // Choose audioStream based on theme
            switch (selectedTheme)
            {
                // Animals theme is selected
                case 0:
                    audioStream = SelectRandomSong(animalsSongs);
                    break;
                // LOTR theme is selected
                case 1:
                    audioStream = SelectRandomSong(lotrSongs);
                    break;
                // Starwars theme is selected
                case 2:
                    audioStream = SelectRandomSong(starWarsSongs);
                    break;
            }

            WaveStream mainOutputStream = new Mp3FileReader(audioStream);
            // Using LoopStream to loop the audio
            LoopStream loop = new LoopStream(mainOutputStream);

            BackgroundPlayer.Init(loop);
            BackgroundPlayer.Play();

        }

        // Stops current BackgroundPlayer playback
        public static void StopBackGroundMusic()
        {
            BackgroundPlayer.Stop();
        }

        // Selects a random song per theme
        private static Stream SelectRandomSong(List<byte[]> songs)
        {
            Random random = new Random();
            int selectedIndex = random.Next(songs.Count);
            return ByteArrayToStream(songs[selectedIndex]);
        }

        // Converts byte array into MemoryStream for playback with NAudio
        private static Stream ByteArrayToStream(byte[] byteArray)
        {
            // Converts byte[] (MP3 file) to Stream
            return new MemoryStream(byteArray);
        }
    }
}
