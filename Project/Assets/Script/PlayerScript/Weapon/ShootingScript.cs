using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _bullets;
    [SerializeField] private float[] _speed;
    [SerializeField] private Transform[] _muzzle;
    [SerializeField] private GameObject[] _weapons;
    [Header("Weapon Timer")]
    [SerializeField] private float[] _timeWeapon;
    [SerializeField] private Slider _weaponSlider;
    [SerializeField] private GameObject[] _weaponIcon;
    [Header("Ammo")]
    [SerializeField] private Text _ammoText;
    [SerializeField] private int[] _weaponAmmo;
    [SerializeField] private int[] _weaponClip;
    [Header("Weapon")]
    [SerializeField] private int _currentWeapon = 2;
    [SerializeField] private Animator _anima;

    private int _currentAmmo;
    private int _currentClip;
    private float _currentTimer;

    private CursorScript _cursor;
    private WeaponSoundScript _sound;

    private void Awake()
    {
        _cursor = FindObjectOfType<CursorScript>();
        _sound = FindObjectOfType<WeaponSoundScript>();
        WeaponSetting();
    }

    private void FixedUpdate()
    {
        Vector3 target = _cursor.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target);
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            Weapon();
        }
    }

    /// <summary>
    /// Реализация выстрела пистолета
    /// </summary>
    private void Pistols()
    {
        _currentTimer -= Time.deltaTime;
        _weaponSlider.value = _currentTimer * -1;
        if (Input.GetKeyDown(KeyCode.Mouse0) && _currentTimer < 0)
        {
            if (_currentClip == 0)
            {
                if (_currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    _currentClip = _weaponClip[_currentWeapon];
                    _currentAmmo -= _currentClip;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo - _weaponClip[_currentWeapon] < 0 && _currentAmmo > 0)
                {
                    _currentClip = _currentAmmo;
                    _currentAmmo = 0;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo == 0)
                {

                }
            }
            else if (_currentClip > 0)
            {
                GameObject bullet = Instantiate(_bullets[0], _muzzle[0].position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * _speed[0]);
                _currentTimer = _timeWeapon[0];
                _weaponSlider.minValue = _currentTimer * -1;
                _currentClip--;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponShoot(0);
            }
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1) && _currentTimer < 0)
        {
            StartCoroutine(PistolAlternative());
        }
    }

    /// <summary>
    /// Реализация альтернативного режима пистолета
    /// </summary>
    /// <returns></returns>
    IEnumerator PistolAlternative()
    {
        if (_currentClip == 0)
        {
            if (_currentAmmo - _weaponClip[_currentWeapon] >= 0)
            {
                _currentClip = _weaponClip[_currentWeapon];
                _currentAmmo -= _currentClip;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponRecharge(_currentWeapon);

            }
            else if (_currentAmmo - _weaponClip[_currentWeapon] < 0 && _currentAmmo > 0)
            {
                _currentClip = _currentAmmo;
                _currentAmmo = 0;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponRecharge(_currentWeapon);
            }
            else if (_currentAmmo == 0)
            {

            }
        }
        else if (_currentClip > 0)
        {
            _currentTimer = _timeWeapon[1];
            _weaponSlider.minValue = _currentTimer * -1;
            for (int i = 0; i < 3; i++)
            {
                if (_currentClip > 0)
                {
                    yield return new WaitForSeconds(0.1f);
                    GameObject bullet = Instantiate(_bullets[0], _muzzle[0].position, transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * _speed[0]);
                    _currentClip--;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponShoot(1);
                }
                else if( _currentClip == 0)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    /// <summary>
    /// Реализация выстрела ружья
    /// </summary>
    private void Gun()
    {
        _currentTimer -= Time.deltaTime;
        _weaponSlider.value = _currentTimer * -1;
        if (Input.GetKeyDown(KeyCode.Mouse0) && _currentTimer < 0)
        {
            if (_currentClip == 0)
            {
                if (_currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    _currentClip = _weaponClip[_currentWeapon];
                    _currentAmmo -= _currentClip;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);

                }
                else if (_currentAmmo - _weaponClip[_currentWeapon] < 0 && _currentAmmo > 0)
                {
                    _currentClip = _currentAmmo;
                    _currentAmmo = 0;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo == 0)
                {

                }
            }
            else if (_currentClip > 0)
            {
                Vector3 pos = new Vector3(-0.3f, 0, 1);
                for(int i = 0; i < 5; i++)
                {
                    GameObject bullet = Instantiate(_bullets[1], _muzzle[1].position,transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(pos * _speed[0]);
                    pos.x+=0.15f;
                    Destroy(bullet, 0.3f);
                }
                _currentTimer = _timeWeapon[2];
                _weaponSlider.minValue = _currentTimer * -1;
                _currentClip--;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponShoot(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && _currentTimer < 0)
        {
            if (_currentClip == 0)
            {
                if (_currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    _currentClip = _weaponClip[_currentWeapon];
                    _currentAmmo -= _currentClip;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);

                }
                else if (_currentAmmo - _weaponClip[_currentWeapon] < 0 && _currentAmmo > 0)
                {
                    _currentClip = _currentAmmo;
                    _currentAmmo = 0;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo == 0)
                {

                }
            }
            else if (_currentClip > 1)
            {
                Vector3 pos = new Vector3(-0.5f, 0, 1);
                for (int i = 0; i < 11; i++)
                {
                    GameObject bullet = Instantiate(_bullets[1], _muzzle[1].position, transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(pos * _speed[0]);
                    pos.x += 0.1f;
                    Destroy(bullet, 0.3f);
                }
                _currentTimer = _timeWeapon[3];
                _weaponSlider.minValue = _currentTimer * -1;
                _currentClip-=2;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponShoot(3);
            }
            else if(_currentClip > 0 && _currentClip < 2)
            {
                Vector3 pos = new Vector3(-0.5f, 0, 1);
                for (int i = 0; i < 5; i++)
                {
                    GameObject bullet = Instantiate(_bullets[1], _muzzle[1].position, transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(pos * _speed[0]);
                    pos.x += 0.25f;
                    Destroy(bullet, 0.3f);
                }
                _currentTimer = _timeWeapon[2];
                _weaponSlider.minValue = _currentTimer * -1;
                _currentClip--;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponShoot(2);
            }
        }
    }

    /// <summary>
    /// Реализация плазматического ружья
    /// </summary>
    private void PlasmaGun()
    {
        _currentTimer -= Time.deltaTime;
        _weaponSlider.value = _currentTimer * -1;
        if (Input.GetKey(KeyCode.Mouse0) && _currentTimer < 0)
        {
            if (_currentClip == 0)
            {
                if (_currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    _currentClip = _weaponClip[_currentWeapon];
                    _currentAmmo -= _currentClip;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo - _weaponClip[_currentWeapon] < 0 && _currentAmmo > 0)
                {
                    _currentClip = _currentAmmo;
                    _currentAmmo = 0;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo == 0)
                {

                }
            }
            else if (_currentClip > 0)
            {
                float x = UnityEngine.Random.Range(-0.2f, 0.2f);
                Vector3 pos = new Vector3(x, 0, 1);
                GameObject bullet = Instantiate(_bullets[2], _muzzle[2].position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(pos * _speed[0]);
                _currentTimer = _timeWeapon[4];
                _weaponSlider.minValue = _currentTimer * -1;
                _currentClip--;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponShoot(4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && _currentTimer < 0 && _currentClip == 0 || 
            Input.GetKeyDown(KeyCode.Mouse1) && _currentTimer < 0 && _currentClip > 9)
        {
            if (_currentClip == 0)
            {
                if (_currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    _currentClip = _weaponClip[_currentWeapon];
                    _currentAmmo -= _currentClip;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo - _weaponClip[_currentWeapon] < 0 && _currentAmmo > 0)
                {
                    _currentClip = _currentAmmo;
                    _currentAmmo = 0;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo == 0)
                {

                }
            }
            else if (_currentClip > 0)
            {
                GameObject bullet = Instantiate(_bullets[3], _muzzle[2].position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * _speed[0]);
                _currentTimer = _timeWeapon[5];
                _weaponSlider.minValue = _currentTimer * -1;
                _currentClip-=10;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponShoot(5);
            }
        }
    }

    /// <summary>
    /// Реализация альтиреристического ружья
    /// </summary>
    private void AltimetricGun()
    {
        _muzzle[5].localEulerAngles = new Vector3(-60, 0, 0);
        _currentTimer -= Time.deltaTime;
        _weaponSlider.value = _currentTimer * -1;
        if (Input.GetKey(KeyCode.Mouse0) && _currentTimer < 0)
        {
            if (_currentClip == 0)
            {
                if (_currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    _currentClip = _weaponClip[_currentWeapon];
                    _currentAmmo -= _currentClip;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);

                }
                else if (_currentAmmo - _weaponClip[_currentWeapon] < 0 && _currentAmmo > 0)
                {
                    _currentClip = _currentAmmo;
                    _currentAmmo = 0;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo == 0)
                {

                }
            }
            else if (_currentClip > 0)
            {
                if (_currentClip % 2 == 0)
                {
                    GameObject bullet = Instantiate(_bullets[4], _muzzle[3].position, transform.rotation);
                }
                else
                {
                    GameObject bullet = Instantiate(_bullets[5], _muzzle[4].position, transform.rotation);
                }
                _currentTimer = _timeWeapon[6];
                _weaponSlider.minValue = _currentTimer * -1;
                _currentClip--;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponShoot(6);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && _currentTimer < 0 && _currentClip == 0 || 
            Input.GetKeyDown(KeyCode.Mouse1) && _currentTimer < 0 && _currentClip > 4)
        {
            if (_currentClip == 0)
            {
                if (_currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    _currentClip = _weaponClip[_currentWeapon];
                    _currentAmmo -= _currentClip;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);

                }
                else if (_currentAmmo - _weaponClip[_currentWeapon] < 0 && _currentAmmo > 0)
                {
                    _currentClip = _currentAmmo;
                    _currentAmmo = 0;
                    _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                    _sound.WeaponRecharge(_currentWeapon);
                }
                else if (_currentAmmo == 0)
                {

                }
            }
            else if (_currentClip > 0)
            {
                Vector3 fromTo = _cursor.transform.position - _muzzle[5].position;
                Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);
                float x = fromToXZ.magnitude;
                float y = fromTo.y;
                float g = Physics.gravity.y;
                float AngleRadians = 60f * Mathf.PI / 180;
                float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleRadians) * x) * Mathf.Pow((Mathf.Cos(AngleRadians)), 2));
                float v = Mathf.Sqrt(Mathf.Abs(v2));
                GameObject bullet = Instantiate(_bullets[6], _muzzle[5].position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = _muzzle[5].forward * v;
                _currentTimer = _timeWeapon[7];
                _weaponSlider.minValue = _currentTimer * -1;
                _currentClip -= 5;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _sound.WeaponShoot(7);
            }
        }
    }

    /// <summary>
    /// Реализация разного оружия
    /// </summary>
    private void Weapon()
    {
        switch (_currentWeapon)
        {
            case 0:
                Pistols();
                break;
            case 1:
                Gun();
                break;
            case 2:
                PlasmaGun();
                break;
            case 3:
                AltimetricGun();
                break;
        }

        if (Input.GetKeyDown(KeyCode.R) && _currentTimer < 0)
        {
            if (_currentClip < _weaponClip[_currentWeapon] && _currentAmmo > 0 && _currentAmmo - _weaponClip[_currentWeapon] >= 0
                || _currentClip < _weaponClip[_currentWeapon] && _currentAmmo > 0 && _currentAmmo - _weaponClip[_currentWeapon] < 0
                && _currentClip + _currentAmmo > _weaponClip[_currentWeapon])
            {
                _currentAmmo -= (_weaponClip[_currentWeapon] - _currentClip);
                _currentClip = _weaponClip[_currentWeapon];
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _currentTimer = _timeWeapon[_currentWeapon];
                _sound.WeaponRecharge(_currentWeapon);
            }
            else if (_currentClip < _weaponClip[_currentWeapon] && _currentAmmo > 0 && _currentAmmo - _weaponClip[_currentWeapon] < 0
                && _currentClip + _currentAmmo <= _weaponClip[_currentWeapon])
            {
                _currentClip += _currentAmmo;
                _currentAmmo = 0;
                _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
                _currentTimer = _timeWeapon[_currentWeapon];
                _sound.WeaponRecharge(_currentWeapon);
            }
        }
    }

    /// <summary>
    /// Реализация смены оружия при бонусе
    /// </summary>
    /// <param name="bonus"></param>
    public void NewWeapon(int bonus)
    {
        _currentWeapon = bonus;
        _sound.WeaponRecharge(bonus);
        WeaponSetting();
    }

    /// <summary>
    /// Настройки оружия в инвентаре и на руках
    /// </summary>
    private void WeaponSetting()
    {
        _currentClip = _weaponClip[_currentWeapon];
        _currentAmmo = _weaponAmmo[_currentWeapon] - _currentClip;
        _ammoText.text = $"Ammo: {_currentClip}/{_currentAmmo}";
        _currentTimer = _timeWeapon[_currentWeapon];
        _weaponSlider.minValue = _currentTimer * -1;
        _cursor.CursorWeapon(_currentWeapon);
        _anima.SetInteger("Weapons", _currentWeapon);
        for (int i = 0; i < _weaponIcon.Length; i++)
        {
            if (i == _currentWeapon)
            {
                _weaponIcon[i].SetActive(true);
                _weapons[i].SetActive(true);
            }
            else
            {
                _weaponIcon[i].SetActive(false);
                _weapons[i].SetActive(false);
            }
        }
    }
}
