using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class TestAnimation : MonoBehaviour {
    
	void Start () {
        UnityFactory.factory.LoadDragonBonesData("RedSoldier/RedSoldier_ske");
        UnityFactory.factory.LoadTextureAtlasData("RedSoldier/RedSoldier_tex");

        var armatureComponent = UnityFactory.factory.BuildArmatureComponent("RedSoldier");

        armatureComponent.animation.Play("Run");

        armatureComponent.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
}
