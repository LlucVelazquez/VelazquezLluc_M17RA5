using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public int maxAmmo = 10;
    private int currentAmmo;
    private PlayerLocomotionInput _playerLocomotionInput;

    private void Awake()
    {
        _playerLocomotionInput = GetComponentInParent<PlayerLocomotionInput>();
    }
    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    private void Update()
    {
        if(currentAmmo <=0)
        {
            return;
        }
        if (_playerLocomotionInput.Shoot)
        {
            Shoot();
            currentAmmo--;
        }
    }
    public void Shoot()
    {
        Debug.Log("Pew Pew");
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

        }
    } 
}
