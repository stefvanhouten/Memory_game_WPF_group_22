using Newtonsoft.Json;
using Security;
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
/// <summary>
/// Provides basic functionality for handling files.
/// </summary>
static class Files
{

    /// <summary>
    /// Create the file based on whether it already exists or not
    /// If exists, ignore the creation of it
    /// if not, create the file in the designated filepath and dispose it after construction
    /// </summary>
    public static void Create(string completePath)
    {
        if (!Files.FileExists(completePath))
        {
            using (FileStream fs = File.Create(completePath))
            {
                /*
                 * "using" statement is being used here, to dispose the ongoing process once it's finished
                 * This ensures the next process won't be halted due to the file being open.
                 */
            }
        }
    }


    /// <summary>
    /// WriteToFile writes data to a file as type string
    /// parameter overwrite will overwrite the file its contents with the new contents passed along
    /// parameter overwrite is default set to false, preventing files from being overwritten
    /// </summary>
    /// <param name="writeMeToFile"></param>
    /// <param name="overwrite"></param>
    public static void WriteToFile(string completePath, string writeMeToFile)
    {
        byte[] data = Encryptor.Encrypt(writeMeToFile);
        File.WriteAllBytes(completePath, data);
    }


    /// <summary>
    /// WriteToFile writes data to a file as type byte[]
    /// parameter overwrite will overwrite the file its contents with the new contents passed along
    /// parameter overwrite is default set to false, preventing files from being overwritten
    /// </summary>
    /// <param name="writeMeToFile"></param>
    /// <param name="overwrite"></param>
    public static void WriteToFile(string completePath, byte[] writeMeToFile, bool overwrite = false)
    {
        /*
         * Overwrites the file
         */
        if (overwrite)
        {
            File.WriteAllBytes(completePath, writeMeToFile);
        }
        else
        {
            /*
             * Append the text to the file and dispose the process.
             * Disposing the process prevents the code from coming to halt when the same file gets accessed again
             * ADDINITIONAL INFO:
             * StreamWriter implements a TextWriter for writing characters to a stream in a particular encoding.
             */
            using (FileStream fs = new FileStream(completePath, FileMode.Open, FileAccess.Write))
            {
                /*
                 * Adds text to the file
                 */
                fs.Write(writeMeToFile, 0, writeMeToFile.Length);
            }
        }
    }


    /// <summary>
    /// Returns the content that is stored in the file
    /// </summary>
    /// <returns>string Content stored in file</returns>
    public static string GetStringFromFileContent(string completePath)
    {
        string data = Convert.ToBase64String(File.ReadAllBytes(completePath));
        if (data.Length > 0)
        {
            return Encryptor.Decrypt(data);
        }
        return data;
    }

    public static byte[] GetBytesFromFileContent(string completePath)
    {
        return File.ReadAllBytes(completePath);
    }

    /// <summary>
    /// Returns the content that is stored in the file
    /// </summary>
    /// <returns>byte[] Content stored in file</returns>
    public static byte[] GetBytesFromFileContent(string completePath)
    {
        return File.ReadAllBytes(completePath);
    }

    /// <summary>
    /// Check whether file exists
    /// </summary>
    /// <returns>Boolean based on if file exists</returns>
    public static bool FileExists(string completePath)
    {
        return File.Exists(completePath);
    }
}