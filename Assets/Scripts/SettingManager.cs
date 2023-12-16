//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Audio;
//using UnityEngine.UI;

//public class SettingManager : MonoBehaviour
//{
//    private float BGMVolumn;

//    public AudioMixer BGMMixer;

//    public GameObject BGMVolumnSliderGameObject;
//    Slider BGMVolumnSlider;
    

//    // Start is called before the first frame update
//    void Start()
//    {
//        BGMMixer.GetFloat("BGM_volumn", out BGMVolumn);
//        BGMVolumnSlider = BGMVolumnSliderGameObject.GetComponent<Slider>();
//        BGMVolumnSlider.value = BGMVolumn;
//    }

//    public void SetBGMVolumn(float volumn)
//    {
//        BGMVolumn = volumn;
//        BGMMixer.SetFloat("BGM_volumn", volumn);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
