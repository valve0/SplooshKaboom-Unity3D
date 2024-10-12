
using System;
using UnityEngine;

namespace Model
{
    public class Squid
    {
        public Squid(int length)
        {
            _length = length;
            HitCounter = 0;
        }

        private int _length;

        private int _hitCounter;
        public bool IsAlive = true;

        public int HitCounter
        {
            get => _hitCounter;

            set
            {
                if (value == _length && IsAlive)
                {
                    IsAlive = false;
                    SquidKilled?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    _hitCounter = value;
                }
            }
        }


        public event EventHandler SquidKilled = delegate{};
    }
}
