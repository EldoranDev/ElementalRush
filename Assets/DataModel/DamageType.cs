using UnityEngine;
using System.Collections;

public enum DamageType {
	Physical,
	Fire,
	Water,
	Nature,
}

public static class DamageTypeExtension {
	public static float GetDamageModifier(this DamageType type, DamageType t) {
		switch (type) {
		case DamageType.Physical:
			return 1f;
		case DamageType.Nature:
			switch (t) {
			case DamageType.Water:
				return 2f;
			case DamageType.Nature:
				return 0f;
			case DamageType.Fire:
				return 0.5f;
			}
			break;
		case DamageType.Fire:
			switch (t) {
			case DamageType.Water:
				return 0.5f;
			case DamageType.Nature:
				return 2f;
			case DamageType.Fire:
				return 0f;
			}
			break;

		case DamageType.Water:
			switch (t) {
			case DamageType.Water:
				return 0f;
			case DamageType.Nature:
				return 0.5f;
			case DamageType.Fire:
				return 2f;
			}
			break;
		}

		return 1f;
	}
}