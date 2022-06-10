<template>
  <v-dialog persistent class="perinfopage__dialog" v-model="perDialog">
    <v-card class="perinfopage__form">
      <v-card-title>Ваши персональные данные</v-card-title>
      <v-form
        class="postform"
        ref="form"
        v-model="valid"
        lazy-validation
        @submit="change"
      >
        <v-file-input
          class="postform__file-imput"
          v-model="file"
          value="1"
          accept="image/*"
          label="Аватар"
          @change="handleImage"
        ></v-file-input>
        <v-text-field
          v-model="name"
          :rules="nameRules"
          label="Имя*"
          required
          @change="validate"
        ></v-text-field>
        <v-text-field
          v-model="email"
          :rules="emailRules"
          label="Email*"
          required
          @change="validate"
        ></v-text-field>
        <v-text-field
          v-model="number"
          :rules="numberRules"
          label="Телефон*"
          required
          @change="validate"
        ></v-text-field>
        <v-text-field
          v-model="status"
          label="Статус"
          @change="validate"
        ></v-text-field>
        <v-card-text class="text-h6">Страна</v-card-text>
        <v-select
          v-model="country"
          :items="countries"
          menu-props="auto"
          label=""
          hide-details
          prepend-icon="mdi-map"
          single-line
        ></v-select>
        <v-card-text class="text-h5">Возраст:{{ this.age }}</v-card-text>
        <v-card-text class="text-h6"
          >Для изменения возраста выберете новую дату рождения</v-card-text
        >
        <v-date-picker
          @change="calculateAge"
          v-model="date_of_bir_str"
        ></v-date-picker>
      </v-form>
      <v-card-text>*-обязательные поля</v-card-text>
      <v-card-actions id="perinfopage__buttons">
        <v-btn color="red darken-1" text @click="handleDelete">
          Удалить аккаунт
        </v-btn>
        <v-btn color="red darken-1" text @click="handleLogOut"> Выйти </v-btn>

        <v-btn color="green darken-1" text @click="change" v-if="valid">
          Изменить
        </v-btn>
      </v-card-actions>
      <v-btn
        color="black darken-1"
        class="perinfopage__dialog-close"
        icon
        text
        @click="closedialog"
        ><v-icon>mdi-close</v-icon></v-btn
      >
    </v-card>
  </v-dialog>
</template>

<script>
import { HTTP } from "../api/API";

export default {
  data: () => ({
    perDialog: false,
    valid: false,
    file: [],
    imgurl: { url: "" },
    name: "",
    email: "",
    country: "Russian Federation",
    countries: [],
    countryNumber: 0,
    age: 0,
    today: new Date(Date.now() - new Date().getTimezoneOffset() * 60000),
    date_of_bir_str: new Date(
      Date.now() - new Date().getTimezoneOffset() * 60000
    )
      .toISOString()
      .substr(0, 10),
    status: "",
    number: "",
    nameRules: [(v) => !!v || "Имя необходимо"],
    emailRules: [
      (v) => !!v || "E-mail необходим",
      (v) => /.+@.+\..+/.test(v) || "E-mail должен быть верным",
    ],
    numberRules: [
      (v) =>
        /^([+]?[\s0-9]+)?(\d{3}|[(]?[0-9]+[)])?([-]?[\s]?[0-9])+$/.test(v) ||
        "Телефон должен быть верным",
    ],
  }),

  methods: {
    handleImage() {
      if (this.file) {
        const reader = new FileReader();
        let rawImg;
        reader.onloadend = () => {
          rawImg = reader.result;
          this.imgurl.url = rawImg;
          console.log(this.imgurl.url);
        };
        reader.readAsDataURL(this.file);
      } else {
        console.log("No img");
        this.imgurl.url = "";
      }
    },
    validate() {
      this.$refs.form.validate();
    },
    reset() {
      this.name = ``;
      this.email = ``;
      this.countryNumber = 0;
      this.status = ``;
      this.age = 0;
      this.number = "";
    },
    change() {
      console.log(this.imgurl.url);
      const token = localStorage.getItem("token");
      HTTP.put(
        "UpdatePerson",
        {
          id: this.$store.getters.GET_CURRUSER.id,
          email: this.email,
          name: this.name,
          status: this.status,
          country: Number(this.countryNumber),
          age: this.age,
          number: this.number,
          avatar: this.imgurl.url,
        },
        {
          headers: {
            Authorization: `Basic ${token}`,
          },
        }
      )
        .then(() => {
          this.closedialog();
          this.getName();
        })
        .catch((err) => {
          console.log(err);
          this.$store.dispatch("SET_ERRDIALOG", true);
        });
    },
    closedialog() {
      this.$store.dispatch("SET_PERDIALOG", false);
    },
    calculateAge() {
      const age =
        this.today.getFullYear() - new Date(this.date_of_bir_str).getFullYear();
      const month =
        this.today.getMonth() - new Date(this.date_of_bir_str).getMonth();
      if (
        month < 0 ||
        (month === 0 &&
          this.today.getDate() < new Date(this.date_of_bir_str).getDate())
      ) {
        this.age =
          this.today.getFullYear() -
          new Date(this.date_of_bir_str).getFullYear() -
          1;
      } else this.age = age;
      console.log(this.age);
    },
    setCountries() {
      require("iso-3166-1")
        .all()
        .forEach((e) => {
          this.countries.push(e.country);
        });
    },
    handleDelete() {
      const token = localStorage.getItem("token");
      HTTP.delete("DeleteAccountFromID", {
        headers: {
          Authorization: `Basic ${token}`,
        },
        params: {
          id: this.$store.getters.GET_CURRUSER.id,
        },
      })
        .then(() => {
          this.handleLogOut();
        })
        .catch((err) => {
          console.log(err);
        });
      this.handleLogOut();
    },
    handleLogOut() {
      localStorage.setItem("token", "");
      this.$router.push("/login");
    },
    async setData() {
      const token = localStorage.getItem("token");
      console.log(token);
      HTTP.get("GetPerson", {
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.age = res.data.age;
          this.countryNumber = res.data.country;
          this.email = res.data.email;
          this.name = res.data.name;
          this.number = res.data.number;
          this.status = res.data.status;
          this.country = require("iso-3166-1").whereNumeric(
            this.countryNumber
          ).country;
        })
        .catch((err) => {
          console.log(err);
        });
    },
  },

  props: {
    getName: {
      required: true,
    },
  },
  watch: {
    "$store.state.perDialog": {
      handler: function () {
        this.perDialog = this.$store.getters.GET_PERDIALOG;
      },
      immediate: true, // provides initial (not changed yet) state
    },
  },
  mounted() {
    this.setCountries();
    this.setData();
  },
};
</script>
<style scoped>
.perinfopage__dialog-close {
  position: fixed;
  top: 40px;
  right: 40px;
}
.perinfopage__dialog {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 70%;
  text-align: center;
}
.perinfopage__form {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  text-align: center;
}
.postform__file-imput {
  padding: 10px 50px;
}
.postform {
  padding: 50px;
}
</style>
