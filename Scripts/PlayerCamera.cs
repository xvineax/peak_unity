using UnityEngine;

public class PlayerCamera : MonoBehaviour 
{        
    [SerializeField] GameObject player;
    [SerializeField] float mouseSense = 2f; 
    
    // Удобнее настраивать от -90 (вверх) до 90 (вниз)
    [SerializeField] float lookUpLimit = -80f;
    [SerializeField] float lookDownLimit = 80f;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {     
        // 1. Получаем движение мыши
        float rotateX = Input.GetAxis("Mouse X") * mouseSense;
        float rotateY = Input.GetAxis("Mouse Y") * mouseSense;

        // 2. Поворачиваем тело игрока влево-вправо
        player.transform.Rotate(Vector3.up * rotateX);

        // 3. Наклоняем саму камеру вверх-вниз
        // Мы используем localRotation, чтобы наклон считался относительно игрока
        transform.Rotate(Vector3.left * rotateY);

        // --- ИСПРАВЛЕНИЕ УГЛОВ (Чтобы камеру не заносило) ---
        
        Vector3 currentRotation = transform.localEulerAngles;

        // Если угол больше 180 (например 350), превращаем его в отрицательный (-10)
        // Это нужно, чтобы Mathf.Clamp понимал, где "верх", а где "низ"
        float angleX = currentRotation.x;
        if (angleX > 180) angleX -= 360;

        // Ограничиваем наклон
        angleX = Mathf.Clamp(angleX, lookUpLimit, lookDownLimit);

        // Возвращаем исправленный угол камере, обнуляя Z (чтобы не было "пьяной" камеры)
        transform.localEulerAngles = new Vector3(angleX, 0, 0);
    }
}