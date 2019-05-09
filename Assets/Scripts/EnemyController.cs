using System.Collections;
using UnityEngine;
using UnityEngine.AI;
namespace TAB { 
    public class EnemyController : MonoBehaviour
    {
        // Предварительная версия скрипта
        public int damage = 1; //Урон
        public float defaultSpeed = 1.5f; //Стандартная скорость
        public float combatRange = 2f; //расстояние удара
        public float atatckDelay = 4f;//Задержка атаки

        public int AttackAnimCount;//Количество анимацй Атаки
        public int WalkAnimCount;//Количество анимаций ходьбы

        [SerializeField]
        private GameObject Target; // Цель
        private float speed; // Конечная скорость
        [SerializeField]
        private int walkAnimNumber; // выбранная анимация ходьбы

        // на будущее переписать все флаги в enum состояния и привести код в порядок

        [SerializeField]
        private bool attack = true; //Флаг атаки
        [SerializeField]
        private bool combat = false; // флаг боя 
        public bool stopped = false; // флаг остановки
        public bool isFlanking;
        public bool Runner = false;
        public float runModifier = 6f;
        private State currentState = State.persecution;
        Animator anim; //ссылкка на Аниматор
        NavMeshAgent nav; // ссылка на NavMeshAgent
        

        public enum State {
            persecution,
            flanking
        }

        void Awake()
        {
            anim = GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();

        }

        public void Start()
        {
            //Получаем цель, задаем рандомную скорость и анимацию
            Target = GameObject.FindGameObjectWithTag("CurrentPlayer");
            walkAnimNumber = Random.Range(1, WalkAnimCount + 1);
            if (walkAnimNumber == WalkAnimCount&&Runner)
            {
                speed = defaultSpeed * runModifier;
            }
            else
            {
                speed = defaultSpeed;
            }
            nav.Warp(transform.position);

            if (isFlanking) { 
            StartCoroutine(CheckState());
            }
        }



        void Update()
        {

            if (currentState == State.persecution || Target == null)
            {
                PersecutionState();
            }
            Animation();


        }

        IEnumerator CheckState() {
            while (true)
            {
                yield return new WaitForSeconds(3f);
                if (Target == null || stopped) break;
                float dist = Vector3.Distance(transform.position, Target.transform.position);
                if (dist > 20.0f)
                {
                    currentState = (State)Random.Range(0, 2);
                    Debug.Log(currentState.ToString());
                }
                else {
                    currentState = State.persecution;
                }

                if (currentState == State.flanking)
                {
                    Vector3 targetDir = Target.transform.position - transform.position;
                    float angle = Vector3.Angle(targetDir, Vector3.forward);
                    float RandomAngle = Random.Range(-70f, 70f);
                    Vector3 pos = transform.position + new Vector3(-1 * Mathf.Sin(angle+ RandomAngle), 0, Mathf.Cos(angle+ RandomAngle)) * 10f;
                    
                    GoToPosition(pos, speed);
                }
                else if (currentState == State.persecution)
                {
                    GoToPosition(Target.transform.position, speed);
                }

            }
        }

        public void PersecutionState() {
            //если нет цели то ищем цель, или идем к цели
            if (Target != null && !stopped)
            {
                GoToPosition(Target.transform.position, speed);
            }
            else
            {
                SetTarget();
            }

            //если цель близко, влючаем флаг боя
            if (Vector3.Distance(transform.position, nav.destination) < combatRange)
            {

                nav.isStopped = true;
                nav.speed = 0;
                if (!combat) { combat = true; }
            }
            else
            {
                combat = false;
            }


            //если флаг боя и цель ещё не отъехала, то бьем, очевидно, в противном случае ищем новую цель
            if (combat)
            {
                if (Target == null)
                {
                    SetTarget();
                    return;
                }
                var lookPos = Target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

                if (attack && Target.GetComponent<IDamagable>().Hp > 0)
                {
                    {
                        StartCoroutine("Attack");
                    }
                }
                else if (Target.GetComponent<IDamagable>().Hp <= 0)
                {
                    Target = null;
                    SetTarget();
                }
            }
        }

        //Куротина выполнения атаки
        IEnumerator Attack()
        {
            if (Target != null)
            {

                attack = false;
                stopped = true;
                anim.SetInteger("AnimNumber", Random.Range(1, AttackAnimCount + 1));
                anim.SetTrigger("Attack");
                if (Target == null)
                {
                    SetTarget();
                    yield break;
                }
                Target.GetComponent<IDamagable>().TakeDamage(damage);

                yield return new WaitForSeconds(atatckDelay);
                stopped = false;
                attack = true;
            }
            else
            {
                yield return null;
            }


        }

        // Идем к цели
        public void GoToPosition(Vector3 position, float speed)
        {
            nav.SetDestination(position);
            nav.speed = speed;
            nav.isStopped = false;
        }

        private void SetTarget()
        {
            Target = GameObject.FindGameObjectWithTag("CurrentPlayer");
            //GoToPosition(Target.transform.position, speed);
        }

        //Обраобтка анимации передвижения
        public void Animation()
        {
            if (nav.speed > 0)
            {

                anim.SetInteger("AnimNumber", walkAnimNumber);
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }

        }

        //Обработка попадания, прекращение движения на 1,5 секунды
        public void Hitted() {
            StartCoroutine(StoppedCourotine());
        }

        IEnumerator StoppedCourotine() {
            stopped = true;
            nav.isStopped = true;
            nav.speed = 0;
            yield return new WaitForSeconds(2.5f);
            stopped = false;

        }
    }
}