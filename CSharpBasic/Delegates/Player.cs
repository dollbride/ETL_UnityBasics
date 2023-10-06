using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    internal class Player
    {
        public float hp
        {
            get { return _hp; }
            set 
            {
                if (_hp == value)
                    return;

                _hp = value;
                // 대리자 직접 호출:
                // 구독된 모든 함수를 차례대로 호출한 후 반환값을 마지막 호출한 함수의 반환값으로
                onHpChanged(value);
                // 간접 호출 Invoke:
                // 의미 없는 함수 호출 스택을 통해서 대리자에 등록된 함수들을 호출하기 때문에 
                // 참조하던 원본 데이터가 있는 변수가 수정되더라고 상관 없이 의도한 결과를 낼 수 있음
                onHpChanged.Invoke(value);
                // Null Check 연산자(:?): 해당 변수가 Null이면 그 뒤로는 멤버 접근을 하지 않고 Null 값 반환
                onHpChanged?.Invoke(value); 
            }
        }

        private float _hp;

        // 대리자 타입 정의
        public delegate void OnHPChangedHandeler(float value);
        // event 한정자 :
        // 해당 대리자를 외부에서 구독하기(+=), 구독취소(-=) 기능만 사용할 수 있도록 제한함
        public event OnHPChangedHandeler onHpChanged;

        private PlayerUI _ui;
        public Player() 
        { 
        }
    }
}
