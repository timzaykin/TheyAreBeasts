using UnityEngine;


namespace TAB {

    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 3f;

        private Vector3 moveVector; //Направление движения персонажа
        private Vector3 LookVector; //Направление взгляда персонажа
        public float gravityForece;

        public MobileController mController;
        public MobileController directionController;

        private CharacterController charConroller;
        private Animator animController;

        void Start()
        {
            charConroller = GetComponent<CharacterController>();
            animController = GetComponent<Animator>();

        }

        // Update is called once per frame
        void Update()
        {
            CharacterMove();
            CharacterGravity();
        }

        private void CharacterMove()
        {
            moveVector = Vector3.zero;
            moveVector.x = mController.Vertial() * -1f;
            moveVector.z = mController.Horisontal();
            moveVector.y = gravityForece;
            LookVector = GetDirectionVector();
            var crossVector = Vector3.Cross(LookVector, moveVector);
            animController.SetFloat("Speed", Vector3.Dot(LookVector, moveVector));
            animController.SetFloat("Rotate", crossVector.y * -1);

            transform.rotation = Quaternion.LookRotation(LookVector);
            charConroller.Move(moveVector * speed * Time.deltaTime);
        }


        //Функция для расчета направления в котором находится курсор мыши отностиельно персонажа
        private Vector3 GetDirectionVector()
        {
//#if UNITY_IPHONE || UNITY_ANDROID
//            if (directionController.getDirection() != Vector3.zero) return directionController.getDirection();
//            else if (mController.getDirection() != Vector3.zero) return mController.getDirection();
//            else return Vector3.forward;
//#endif

//#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                var heading = hit.point - transform.position;
                var distance = heading.magnitude;
                var direction = heading / distance;
                return new Vector3(direction.x, 0, direction.z);
            }
            else
            {
                return Vector3.forward;
            }
//#endif
        }

        private void CharacterGravity()
        {
            if (!charConroller.isGrounded) gravityForece -= 20f * Time.deltaTime;
            else gravityForece = -1f;
        }

    }
}
