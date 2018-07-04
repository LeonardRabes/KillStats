using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillStats
{
    class Save
    {
        public static Save FromStream(Stream currentSave)
        {
            MemoryStream mStream = new MemoryStream();
            currentSave.CopyTo(mStream);
            BinaryReader reader = new BinaryReader(mStream);

            ushort S_weaponCount;
            ulong[] S_weaponIDs;
            byte[] S_weaponParts;

            ushort S_totalPointsCount;
            uint S_totalPoints;
            uint[][] S_totalWeaponPoints;

            ushort S_totalDays;
            ulong[] S_days;
            uint[] S_timePerDay;

            uint[][][] S_weaponPointsPerDay;

            //Weapons
            S_weaponCount = reader.ReadUInt16();
            S_weaponIDs = new ulong[S_weaponCount];
            S_weaponParts = new byte[S_weaponCount];
            for (int i = 0; i < S_weaponCount; i++)
            {
                S_weaponIDs[i] = reader.ReadUInt64();
            }
            for (int i = 0; i < S_weaponCount; i++)
            {
                S_weaponParts[i] = reader.ReadByte();
            }

            //Points
            S_totalPointsCount = reader.ReadUInt16();
            S_totalPoints = reader.ReadUInt32();
            S_totalWeaponPoints = new uint[S_weaponCount][];
            for (int iter = 0; iter < S_weaponCount; iter++)
            {
                for (int i = 0; i < S_weaponParts[iter]; i++)
                {
                    S_totalWeaponPoints[iter][i] = reader.ReadUInt32();
                }
            }

            //Days
            S_totalDays = reader.ReadUInt16();
            S_days = new ulong[S_totalDays];
            S_timePerDay = new uint[S_totalDays];
            for (int i = 0; i < S_totalDays; i++)
            {
                S_days[i] = reader.ReadUInt64();
            }
            for (int i = 0; i < S_totalDays; i++)
            {
                S_timePerDay[i] = reader.ReadUInt32();
            }

            //WeaponPointsPerDay
            S_weaponPointsPerDay = new uint[S_totalDays][][];
            for (int iteration = 0; iteration < S_totalDays; iteration++)
            {
                for (int iter = 0; iter < S_weaponCount; iter++)
                {
                    S_weaponPointsPerDay[iter] = new uint[S_weaponCount][];
                    for (int i = 0; i < S_weaponParts[iter]; i++)
                    {
                        S_weaponPointsPerDay[iteration][iter][i] = reader.ReadUInt32();
                    }
                }
            }

            return new Save(S_weaponCount, S_weaponIDs, S_weaponParts, S_totalPointsCount, S_totalPoints, S_totalWeaponPoints, S_totalDays, S_days, S_timePerDay, S_weaponPointsPerDay);
        }

        public static Save Create(string[] _weaponIDs, uint[][][] _points, DateTime _today, int _iterations)
        {
            ushort weaponCount;
            ulong[] weaponIDs;
            byte[] weaponParts;

            ushort totalPointsCount;
            uint totalPoints;
            uint[][] totalWeaponPoints;

            ushort totalDays;
            ulong[] days;
            uint[] timePerDay;

            uint[][][] weaponPointsPerDay;

            //Points
            weaponCount = (ushort)_weaponIDs.Length;
            weaponIDs = new ulong[weaponCount];
            weaponParts = new byte[weaponCount];
            for (int i = 0; i < weaponCount; i++)
            {
                weaponIDs[i] = Convert.ToUInt64(_weaponIDs[i]);
            }

            SortIDs(ref weaponIDs, ref _points);

            for (int i = 0; i < weaponCount; i++)
            {
                weaponParts[i] = (byte)_points[0][i].Length;
            }

            //Points
            totalPointsCount = 0;
            totalPoints = 0;
            foreach (var val in weaponParts)
            {
                totalPointsCount += val;
            }
            foreach (var time in _points)
            {
                foreach (var item in time)
                {
                    foreach (var val in item)
                    {
                        totalPoints += val;
                    }
                }
            }
            totalWeaponPoints = new uint[weaponCount][];
            for (int iter = 0; iter < weaponCount; iter++)
            {
                totalWeaponPoints[iter] = new uint[weaponParts[iter]];
                for (int i = 0; i < weaponParts[iter]; i++)
                {
                    totalWeaponPoints[iter][i] = _points[_points.Length - 1][iter][i] - _points[0][iter][i];
                }
            }

            //Days
            totalDays = 1;
            days = new ulong[1];
            timePerDay = new uint[1];
            days[0] = (ulong)_today.ToBinary();
            timePerDay[0] = (uint)_iterations;

            //WeaponPointsPerDay
            weaponPointsPerDay = new uint[1][][];
            weaponPointsPerDay[0] = totalWeaponPoints;

            return new Save(weaponCount, weaponIDs, weaponParts, totalPointsCount, totalPoints, totalWeaponPoints, totalDays, days, timePerDay, weaponPointsPerDay);
        }

        static int[] SortIDs(ref ulong[] _weaponIDs, ref uint[][][] _points)
        {
            ulong[] newWeaponIDs = new ulong[_weaponIDs.Length];
            uint[][][] newPoints = new uint[_points.Length][][];
            int[] changes = new int[_weaponIDs.Length];
            ulong val = ulong.MaxValue;
            for (int iter = 0; iter < _weaponIDs.Length; iter++)
            {
                for (int i = 0; i < _weaponIDs.Length; i++)
                {
                    if (_weaponIDs[i] < val)
                    {
                        val = _weaponIDs[i];
                        changes[iter] = i; //moved from pos i to iter
                    }
                }

                val = ulong.MaxValue;
                newWeaponIDs[iter] = _weaponIDs[changes[iter]];
                _weaponIDs[changes[iter]] = ulong.MaxValue;
            }

            for (int iter = 0; iter < _points.Length; iter++)
            {
                uint[][] time = new uint[_weaponIDs.Length][];
                for (int i = 0; i < _weaponIDs.Length; i++)
                {
                    uint[] weapon = _points[iter][changes[i]];
                    time[i] = weapon;
                }

                newPoints[iter] = time;
            }

            _weaponIDs = newWeaponIDs;
            _points = newPoints;
            return changes;
        }

        public ushort weaponCount;
        public ulong[] weaponIDs;
        public byte[] weaponParts;

        public ushort totalPointsCount;
        public uint totalPoints;
        public uint[][] totalWeaponPoints;

        public ushort totalDays;
        public ulong[] days;
        public uint[] timePerDay;

        public uint[][][] weaponPointsPerDay;

        public Save()
        {

        }

        private Save(ushort _weaponCount, ulong[] _weaponIDs, byte[] _weaponParts,
                    ushort _totalPointsCount, uint _totalPoints, uint[][] _totalWeaponPoints,
                    ushort _totalDays, ulong[] _days, uint[] _timePerDay, uint[][][] _weaponPointsPerDay)
        {
            weaponCount = _weaponCount;
            weaponIDs = _weaponIDs;
            weaponParts = _weaponParts;

            totalPointsCount = _totalPointsCount;
            totalPoints = _totalPoints;
            totalWeaponPoints = _totalWeaponPoints;

            totalDays = _totalDays;
            days = _days;
            timePerDay = _timePerDay;

            weaponPointsPerDay = _weaponPointsPerDay;
        }

        public void Update(ulong[] _weaponIDs, uint[][][] _points, DateTime _today, int _iterations)
        {
            bool IDsCongruent = true;
            bool partsCongruent = true;
            List<int> found = new List<int>(); // old found IDs from weaponIDs[]
            List<int> notFound = new List<int>(); //not found IDs from weaponIDs[] << old IDs
            List<int> newWeapons = new List<int>(); // new IDs in _weaponIDs[]
            int counter = 0;

            //check, which IDs were and weren't found
            for (int iter = 0; iter < weaponIDs.Length; iter++)
            {
                for (int i = 0; i < _weaponIDs.Length; i++)
                {
                    if (weaponIDs[iter] == _weaponIDs[i])
                    {
                        found.Add(iter);
                        counter++;
                    }
                }
                if (iter + 1 != counter)
                {
                    IDsCongruent = false;
                    notFound.Add(iter);
                    counter++;
                }
            }

            //check if new weaponIDs have been added
            counter = 0;
            for (int iter = 0; iter < _weaponIDs.Length; iter++)
            {
                for (int i = 0; i < weaponIDs.Length; i++)
                {
                    if (_weaponIDs[iter] == weaponIDs[i])
                    {
                        counter++;
                    }
                }

                if (iter + 1 != counter)
                {
                    IDsCongruent = false;
                    newWeapons.Add(iter);
                    counter++;
                }
            }

            for (int i = 0; i < found.Count; i++)
            {
                if (true)
                {

                }
            }

            //uint test = _points
        }

        public Stream ToStream()
        {
            MemoryStream mStream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(mStream);

            //Weapon
            writer.Write(weaponCount);
            foreach (var item in weaponIDs)
            {
                writer.Write(item);
            }
            writer.Write(weaponParts);

            //Points
            writer.Write(totalPointsCount);
            writer.Write(totalPoints);
            foreach (var item in totalWeaponPoints)
            {
                foreach (var val in item)
                {
                    writer.Write(val);
                }
            }

            //Days
            writer.Write(totalDays);
            foreach (var item in days)
            {
                writer.Write(item);
            }
            foreach (var item in timePerDay)
            {
                writer.Write(item);
            }

            //WeaponPointsPerDay
            foreach (var day in weaponPointsPerDay)
            {
                foreach (var weapon in day)
                {
                    foreach (var part in weapon)
                    {
                        writer.Write(part);
                    }

                }
            }

            return mStream;
        }
    }
}
