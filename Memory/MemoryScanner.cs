using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Internals;

namespace Starship.Win32.Memory {
    public class MemoryScanner {

        public MemoryScanner(Process process) {
            Memory = new MemorySharp(process);
        }

        public void Scan(int valueToFind) {
            var regions = Memory.Memory.Regions.ToList();
            var offsets = new List<int>();
            var tasks = new List<Task>();
            var search = 2548;

            foreach(var region in regions) {
                
                tasks.Add(Task.Factory.StartNew(()=> {
                    try {
                        var count = region.Information.RegionSize / MarshalType<int>.Size;
                        var values = region.Read<int>(0, count);
                        var index = 0;

                        foreach(var value in values) {
                            if(value == search) {
                                offsets.Add(region.Information.BaseAddress.ToInt32() + index * MarshalType<int>.Size);
                            }

                            index += 1;
                        }
                    }
                    catch(Win32Exception) {
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            var test = 0;

            foreach(var offset in offsets.OrderBy(each => each)) {
                try {
                    Memory.Write(new IntPtr(offset), search + 10000, false);
                }
                catch(Win32Exception) {
                }

                test += 1;
                //break;
            }
        }

        public T Read<T>() {
            return Memory.Read<T>(Offset);
        }

        private IntPtr Offset { get; set; }

        private MemorySharp Memory { get; set; }
    }
}