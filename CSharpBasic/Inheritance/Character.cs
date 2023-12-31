﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // abstract 키워드 :
    // 추상용도로 사용하는 것이므로 반드시 상속자가 해당 내용을 직접 구현해주어야 한다고 명시. 
    internal abstract class Character : IAttacker
    {
        public string? NickName { get; set; }

        //public string NickName
        //{
        //    get
        //    {
        //        return _nickName;
        //    }
        //    set
        //    {
        //        _nickName = value;
        //    }
        //}
        //private string _nickName;


        public float Exp
        {
            get
            {
                return _exp;
            }
            private set
            {
                if (value < 0)
                    value = 0;

                _exp = value;
            }
        }

        public float AttackPower
        {
            get
            {
                return _attackPower;
            }
        }

        private float _exp;
        private float _attackPower;

        //================================================================================
        //                                Public Methods
        //================================================================================

        public float GetExp() 
        {
            return _exp; 
        }

        public void SetExp(float value)
        {
            if (value < 0)
                value = 0;

            _exp = value;
        }

        public void Jump()
        {
            Console.WriteLine("Jump!");
        }

        public void Attack(IHp target)
        {
            target.DepleteHp(_attackPower);
        }


        //================================================================================
        //                                Private Methods
        //================================================================================
    }
}
