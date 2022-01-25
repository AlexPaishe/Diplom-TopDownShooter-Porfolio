using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingShootScript : MonoBehaviour
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
    [Header("Setting")]
    [SerializeField] private GameObject[] _weaponSphere;
    [SerializeField] private GameObject[] _monsterSphere;
    [SerializeField] private GameObject[] _monsterIcon;
    [SerializeField] private Image[] _frame;
    [Header("Stand")]
    [SerializeField] private GameObject[] _standWeapon;
    [SerializeField] private GameObject[] _standEnemy;
    [SerializeField] private Transform _standSystem;
    [SerializeField] private Transform[] _standPoint;

    private int currentAmmo;
    private int currentClip;
    private float currentTimer;
    private int currentMonster = 0;
    private bool standgo = false;

    private CursorScript cursor;
    private WeaponSoundScript sound;
    private CameraMainMenuScript cam;

    private void Awake()
    {
        cursor = FindObjectOfType<CursorScript>();
        sound = FindObjectOfType<WeaponSoundScript>();
        cam = FindObjectOfType<CameraMainMenuScript>();
        WeaponSetting();
        MonsterSetting();
        _frame[0].color = Color.green;
        _frame[1].color = Color.blue;
    }

    private void FixedUpdate()
    {
        if (cam.ForwardBool() == true)
        {
            Vector3 target = cursor.transform.position;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        if(standgo == true)
        {
            _standSystem.position = Vector3.MoveTowards(_standSystem.position, _standPoint[0].position, 3 * Time.deltaTime);
        }
        else
        {
            _standSystem.position = Vector3.MoveTowards(_standSystem.position, _standPoint[1].position, 3 * Time.deltaTime);
        }
    }

    private void Update()
    {
        InputList();
        if(cam.ForwardBool() == true)
        {
            Weapon();
        }
    }

    /// <summary>
    /// Реализация выстрела пистолета
    /// </summary>
    private void Pistols()
    {
        currentTimer -= Time.deltaTime;
        _weaponSlider.value = currentTimer * -1;
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentTimer < 0)
        {
            if (currentClip == 0)
            {
                if (currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    currentClip = _weaponClip[_currentWeapon];
                    currentAmmo -= currentClip;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo - _weaponClip[_currentWeapon] < 0 && currentAmmo > 0)
                {
                    currentClip = currentAmmo;
                    currentAmmo = 0;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo == 0)
                {
                    NewWeapon(_currentWeapon);
                }
            }
            else if (currentClip > 0)
            {
                GameObject bullet = Instantiate(_bullets[0], _muzzle[0].position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * _speed[0]);
                currentTimer = _timeWeapon[0];
                _weaponSlider.minValue = currentTimer * -1;
                currentClip--;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponShoot(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && currentTimer < 0)
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
        if (currentClip == 0)
        {
            if (currentAmmo - _weaponClip[_currentWeapon] >= 0)
            {
                currentClip = _weaponClip[_currentWeapon];
                currentAmmo -= currentClip;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponRecharge(_currentWeapon);

            }
            else if (currentAmmo - _weaponClip[_currentWeapon] < 0 && currentAmmo > 0)
            {
                currentClip = currentAmmo;
                currentAmmo = 0;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponRecharge(_currentWeapon);
            }
            else if (currentAmmo == 0)
            {
                NewWeapon(_currentWeapon);
            }
        }
        else if (currentClip > 0)
        {
            currentTimer = _timeWeapon[1];
            _weaponSlider.minValue = currentTimer * -1;
            for (int i = 0; i < 3; i++)
            {
                if (currentClip > 0)
                {
                    yield return new WaitForSeconds(0.1f);
                    GameObject bullet = Instantiate(_bullets[0], _muzzle[0].position, transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * _speed[0]);
                    currentClip--;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponShoot(1);
                }
                else if (currentClip == 0)
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
        currentTimer -= Time.deltaTime;
        _weaponSlider.value = currentTimer * -1;
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentTimer < 0)
        {
            if (currentClip == 0)
            {
                if (currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    currentClip = _weaponClip[_currentWeapon];
                    currentAmmo -= currentClip;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);

                }
                else if (currentAmmo - _weaponClip[_currentWeapon] < 0 && currentAmmo > 0)
                {
                    currentClip = currentAmmo;
                    currentAmmo = 0;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo == 0)
                {
                    NewWeapon(_currentWeapon);
                }
            }
            else if (currentClip > 0)
            {
                Vector3 pos = new Vector3(-0.3f, 0, 1);
                for (int i = 0; i < 5; i++)
                {
                    GameObject bullet = Instantiate(_bullets[1], _muzzle[1].position, transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(pos * _speed[0]);
                    pos.x += 0.15f;
                    Destroy(bullet, 0.3f);
                }
                currentTimer = _timeWeapon[2];
                _weaponSlider.minValue = currentTimer * -1;
                currentClip--;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponShoot(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && currentTimer < 0)
        {
            if (currentClip == 0)
            {
                if (currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    currentClip = _weaponClip[_currentWeapon];
                    currentAmmo -= currentClip;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);

                }
                else if (currentAmmo - _weaponClip[_currentWeapon] < 0 && currentAmmo > 0)
                {
                    currentClip = currentAmmo;
                    currentAmmo = 0;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo == 0)
                {
                    NewWeapon(_currentWeapon);
                }
            }
            else if (currentClip > 1)
            {
                Vector3 pos = new Vector3(-0.5f, 0, 1);
                for (int i = 0; i < 11; i++)
                {
                    GameObject bullet = Instantiate(_bullets[1], _muzzle[1].position, transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(pos * _speed[0]);
                    pos.x += 0.1f;
                    Destroy(bullet, 0.3f);
                }
                currentTimer = _timeWeapon[3];
                _weaponSlider.minValue = currentTimer * -1;
                currentClip -= 2;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponShoot(3);
            }
            else if (currentClip > 0 && currentClip < 2)
            {
                Vector3 pos = new Vector3(-0.5f, 0, 1);
                for (int i = 0; i < 5; i++)
                {
                    GameObject bullet = Instantiate(_bullets[1], _muzzle[1].position, transform.rotation);
                    bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(pos * _speed[0]);
                    pos.x += 0.25f;
                    Destroy(bullet, 0.3f);
                }
                currentTimer = _timeWeapon[2];
                _weaponSlider.minValue = currentTimer * -1;
                currentClip--;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponShoot(2);
            }
        }
    }

    /// <summary>
    /// Реализация плазматического ружия
    /// </summary>
    private void PlasmaGun()
    {
        currentTimer -= Time.deltaTime;
        _weaponSlider.value = currentTimer * -1;
        if (Input.GetKey(KeyCode.Mouse0) && currentTimer < 0)
        {
            if (currentClip == 0)
            {
                if (currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    currentClip = _weaponClip[_currentWeapon];
                    currentAmmo -= currentClip;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo - _weaponClip[_currentWeapon] < 0 && currentAmmo > 0)
                {
                    currentClip = currentAmmo;
                    currentAmmo = 0;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo == 0)
                {
                    NewWeapon(_currentWeapon);
                }
            }
            else if (currentClip > 0)
            {
                float x = UnityEngine.Random.Range(-0.2f, 0.2f);
                Vector3 pos = new Vector3(x, 0, 1);
                GameObject bullet = Instantiate(_bullets[2], _muzzle[2].position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(pos * _speed[0]);
                currentTimer = _timeWeapon[4];
                _weaponSlider.minValue = currentTimer * -1;
                currentClip--;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponShoot(4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && currentTimer < 0 && currentClip == 0 || Input.GetKeyDown(KeyCode.Mouse1) && currentTimer < 0 && currentClip > 9)
        {
            if (currentClip == 0)
            {
                if (currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    currentClip = _weaponClip[_currentWeapon];
                    currentAmmo -= currentClip;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo - _weaponClip[_currentWeapon] < 0 && currentAmmo > 0)
                {
                    currentClip = currentAmmo;
                    currentAmmo = 0;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo == 0)
                {
                    NewWeapon(_currentWeapon);
                }
            }
            else if (currentClip > 0)
            {
                GameObject bullet = Instantiate(_bullets[3], _muzzle[2].position, transform.rotation);
                bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * _speed[0]);
                currentTimer = _timeWeapon[5];
                _weaponSlider.minValue = currentTimer * -1;
                currentClip -= 10;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponShoot(5);
            }
        }
    }

    /// <summary>
    /// Реализация альтиреристического ружья
    /// </summary>
    private void AltimetricGun()
    {
        _muzzle[5].localEulerAngles = new Vector3(-60, 0, 0);
        currentTimer -= Time.deltaTime;
        _weaponSlider.value = currentTimer * -1;
        if (Input.GetKey(KeyCode.Mouse0) && currentTimer < 0)
        {
            if (currentClip == 0)
            {
                if (currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    currentClip = _weaponClip[_currentWeapon];
                    currentAmmo -= currentClip;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);

                }
                else if (currentAmmo - _weaponClip[_currentWeapon] < 0 && currentAmmo > 0)
                {
                    currentClip = currentAmmo;
                    currentAmmo = 0;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo == 0)
                {
                    NewWeapon(_currentWeapon);
                }
            }
            else if (currentClip > 0)
            {
                if (currentClip % 2 == 0)
                {
                    GameObject bullet = Instantiate(_bullets[4], _muzzle[3].position, transform.rotation);
                }
                else
                {
                    GameObject bullet = Instantiate(_bullets[5], _muzzle[4].position, transform.rotation);
                }
                currentTimer = _timeWeapon[6];
                _weaponSlider.minValue = currentTimer * -1;
                currentClip--;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponShoot(6);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && currentTimer < 0 && currentClip == 0 || Input.GetKeyDown(KeyCode.Mouse1) && currentTimer < 0 && currentClip > 4)
        {
            if (currentClip == 0)
            {
                if (currentAmmo - _weaponClip[_currentWeapon] >= 0)
                {
                    currentClip = _weaponClip[_currentWeapon];
                    currentAmmo -= currentClip;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);

                }
                else if (currentAmmo - _weaponClip[_currentWeapon] < 0 && currentAmmo > 0)
                {
                    currentClip = currentAmmo;
                    currentAmmo = 0;
                    _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                    sound.WeaponRecharge(_currentWeapon);
                }
                else if (currentAmmo == 0)
                {
                    NewWeapon(_currentWeapon);
                }
            }
            else if (currentClip > 0)
            {
                Vector3 fromTo = cursor.transform.position - _muzzle[5].position;
                Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);
                float x = fromToXZ.magnitude;
                float y = fromTo.y;
                float g = Physics.gravity.y;
                float AngleRadians = 60f * Mathf.PI / 180;
                float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleRadians) * x) * Mathf.Pow((Mathf.Cos(AngleRadians)), 2));
                float v = Mathf.Sqrt(Mathf.Abs(v2));
                GameObject bullet = Instantiate(_bullets[6], _muzzle[5].position, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = _muzzle[5].forward * v;
                currentTimer = _timeWeapon[7];
                _weaponSlider.minValue = currentTimer * -1;
                currentClip -= 5;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                sound.WeaponShoot(7);
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

        if (Input.GetKeyDown(KeyCode.R) && currentTimer < 0)
        {
            if(currentClip < _weaponClip[_currentWeapon] && currentAmmo > 0 && currentAmmo - _weaponClip[_currentWeapon] >=0
                || currentClip < _weaponClip[_currentWeapon] && currentAmmo > 0 && currentAmmo - _weaponClip[_currentWeapon] < 0
                && currentClip + currentAmmo > _weaponClip[_currentWeapon])
            {
                currentAmmo -= (_weaponClip[_currentWeapon] - currentClip);
                currentClip = _weaponClip[_currentWeapon];
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                currentTimer = _timeWeapon[_currentWeapon];
                sound.WeaponRecharge(_currentWeapon);
            }
            else if (currentClip < _weaponClip[_currentWeapon] && currentAmmo > 0 && currentAmmo - _weaponClip[_currentWeapon] < 0 
                && currentClip + currentAmmo <= _weaponClip[_currentWeapon])
            {
                currentClip += currentAmmo;
                currentAmmo = 0;
                _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
                currentTimer = _timeWeapon[_currentWeapon];
                sound.WeaponRecharge(_currentWeapon);
            }
            else if (currentAmmo == 0 && currentClip == 0)
            {
                NewWeapon(_currentWeapon);
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
        sound.WeaponRecharge(bonus);
        WeaponSetting();
    }

    /// <summary>
    /// Настройки оружия в инвентаре и на руках
    /// </summary>
    private void WeaponSetting()
    {
        currentClip = _weaponClip[_currentWeapon];
        currentAmmo = _weaponAmmo[_currentWeapon] - currentClip;
        _ammoText.text = $"Ammo: {currentClip}/{currentAmmo}";
        currentTimer = _timeWeapon[_currentWeapon];
        _weaponSlider.minValue = currentTimer * -1;
        cursor.CursorWeapon(_currentWeapon);
        _anima.SetInteger("Weapons", _currentWeapon);
        for (int i = 0; i < _weaponIcon.Length; i++)
        {
            if (i == _currentWeapon)
            {
                _weaponIcon[i].SetActive(true);
                _weapons[i].SetActive(true);
                _weaponSphere[i].SetActive(true);
                _standWeapon[i].SetActive(true);
            }
            else
            {
                _weaponIcon[i].SetActive(false);
                _weapons[i].SetActive(false);
                _weaponSphere[i].SetActive(false);
                _standWeapon[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Настройка монстров в инвентаре
    /// </summary>
    private void MonsterSetting()
    {
        for (int i = 0; i < _monsterIcon.Length; i++)
        {
            if (i == currentMonster)
            {
                _monsterIcon[i].SetActive(true);
                _monsterSphere[i].SetActive(true);
                _standEnemy[i].SetActive(true);
            }
            else
            {
                _monsterIcon[i].SetActive(false);
                _monsterSphere[i].SetActive(false);
                _standEnemy[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Реализация клавиш в тренировке
    /// </summary>
    private void InputList()
    {
        if (Input.GetKeyDown(KeyCode.W) && cam.ForwardBool() == true)
        {
            _currentWeapon++;
            if (_currentWeapon == 4)
            {
                _currentWeapon = 0;
            }
            sound.WeaponRecharge(_currentWeapon);
            WeaponSetting();
        }
        if (Input.GetKeyDown(KeyCode.S) && cam.ForwardBool() == true)
        {
            _currentWeapon--;
            if (_currentWeapon == -1)
            {
                _currentWeapon = 3;
            }
            sound.WeaponRecharge(_currentWeapon);
            WeaponSetting();
        }

        if (Input.GetKeyDown(KeyCode.D) && cam.ForwardBool() == true)
        {
            currentMonster++;
            if (currentMonster == 4)
            {
                currentMonster = 0;
            }
            MonsterSetting();
        }
        if (Input.GetKeyDown(KeyCode.A) && cam.ForwardBool() == true)
        {
            currentMonster--;
            if (currentMonster == -1)
            {
                currentMonster = 3;
            }
            MonsterSetting();
        }

        if(Input.GetKeyDown(KeyCode.E) && cam.ForwardBool() == true)
        {
            standgo = !standgo;
        }
    }

    /// <summary>
    /// Выключение стенда
    /// </summary>
    public void Stands()
    {
        standgo = false;
    }
}
