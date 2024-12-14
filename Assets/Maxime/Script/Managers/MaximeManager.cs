using UnityEngine;

namespace Maxime.Script.Managers
{
    public class MaximeManager : MonoBehaviour
    {
       public static MaximeManager Imaxime;

       private void Awake()
       {
           if (Imaxime == null)
           {
               Imaxime = this;
               DontDestroyOnLoad(this);
           }
           else
           {
               Destroy(this);
           }
       }
    }
}
