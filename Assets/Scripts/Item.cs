using UnityEngine;


namespace TAB { 
    public class Item : ItemBase
    {
        public int power = 10;



        public Items CurretnItem;

        public enum Items {
            Heal,
            Armour,
            Ammo
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (CurretnItem)
            {
                case Items.Heal:
                    Health(other);
                    break;
                case Items.Armour:
                    Armour(other);
                    break;
                case Items.Ammo:
                    Ammo(other);
                    break;
                default:
                    break;
            }
        }




        private void Ammo(Collider other)
        {
            WeaponManger manger = other.gameObject.GetComponent<WeaponManger>();
            if (manger != null && manger.invenntoryWeapon[1] != null)
            {
                manger.invenntoryWeapon[1].GetComponent<Weapon>().AddAmmo();
                Destroy(gameObject);
            }
        }

        private void Health(Collider other) {
            PlayerHealth hp = other.gameObject.GetComponent<PlayerHealth>();
            if (hp != null) {
                hp.TakeHeal(power);
                Destroy(gameObject);
            }
        }

        private void Armour(Collider other)
        {
            PlayerHealth hp = other.gameObject.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeArmour(power);
                Destroy(gameObject);
            }
        }
    }
}
