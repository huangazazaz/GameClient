using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Msg
{
    public class ControlPanel : UIBase
    {
        Button Throw = null;

        private void Awake()
        {
            Bind(UIEvent.READY_THROW);
        }

        public override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.READY_THROW:
                    Throw.gameObject.SetActive(true);
                    break;
            }
        }

        void Start()
        {
            Throw = transform.Find("throw").GetComponent<Button>();

            Throw.gameObject.SetActive(false);
        }


    }
}
