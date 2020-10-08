﻿using System.IO;
using System.Threading;
using MemoryGame.Properties;
using NAudio.Wave;

namespace MemoryGame
{
    public class Sound
    {
        public static void PlayEffect(byte[] audioResource)
        {
            // Creates WaveStream (inherits from System.IO.Stream), and reads the MP3 file
            WaveStream mainOutputStream = new Mp3FileReader(ByteArrayToStream(audioResource));
            WaveChannel32 volumeStream = new WaveChannel32(mainOutputStream);

            // Creates the player
            WaveOutEvent player = new WaveOutEvent();

            // Add the volumeStream to the player
            player.Init(volumeStream);

            // Start playback
            player.Play();
        }

        public static WaveOutEvent BackgroundPlayer = new WaveOutEvent();

        public static void StartBackgroundMusic(int selectedTheme)
        {
            // Create an empty Stream
            Stream audioStream = new MemoryStream();

            //Choose audioResource based on theme
            switch (selectedTheme)
            {
                case 0:
                    // Animals theme song
                    audioStream = ByteArrayToStream(Resources.bangerbeat);
                    break;
                case 1:
                    // LOTR theme song
                    audioStream = ByteArrayToStream(Resources.lotrtrack);
                    break;
            }

            WaveStream mainOutputStream = new Mp3FileReader(audioStream);
            // Using LoopStream to loop the audio
            LoopStream loop = new LoopStream(mainOutputStream);

            BackgroundPlayer.Init(loop);
            BackgroundPlayer.Play();

        }

        public static void StopBackGroundMusic()
        {
            BackgroundPlayer.Stop();
        }

        private static Stream ByteArrayToStream(byte[] byteArray)
        {
            // Converts byte[] (MP3 file) to Stream
            return new MemoryStream(byteArray);
        }
    }
}