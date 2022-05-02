<template>
  <v-dialog class="perinfopage__dialog" ov v-model="perDialog">
    <v-card class="perinfopage__form">
      <v-card-title>Ваши персональные данные</v-card-title>
      <v-form
        class="postform"
        ref="form"
        v-model="valid"
        lazy-validation
        @submit="change"
      >
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
          v-model="date_of_bir"
        ></v-date-picker>
      </v-form>
      <v-card-text>*-обязательные поля</v-card-text>
      <v-card-actions class="perinfopage__buttons">
        <v-spacer></v-spacer>
        <v-btn color="green darken-1" text @click="change" v-if="valid">
          Изменить
        </v-btn>
        <v-btn color="green darken-1" text @click="closedialog">
          Ок
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import { HTTP } from "../api/API";

export default {
  data: () => ({
    perDialog: false,
    valid: false,
    name: "",
    email: "",
    country: "Russian Federation",
    countries: [],
    countryNumber: 0,
    age: 0,
    date_of_bir: new Date(Date.now() - new Date().getTimezoneOffset() * 60000)
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
      this.$store.dispatch("SET_PERDIALOG", !this.$store.getters.GET_PERDIALOG);
    },
    calculateAge() {
      this.age =
        Number(
          new Date(Date.now() - new Date().getTimezoneOffset() * 60000)
            .toISOString()
            .substr(0, 4)
        ) - Number(this.date_of_bir.substr(0, 4));
    },
    setCountries() {
      require("iso-3166-1")
        .all()
        .forEach((e) => {
          this.countries.push(e.country);
        });
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
          this.age=res.data.age;
          this.countryNumber=res.data.country;
          this.email=res.data.email;
          this.name=res.data.name;
          this.number=res.data.number;
          this.status=res.data.status;
          this.country = require('iso-3166-1').whereNumeric(this.countryNumber).country;
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
.perinfopage__dialog {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 70%;
  text-align: center;
}
.perinfopage__form{
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  text-align: center;
}
.postform {
  padding: 50px;
}
</style>
