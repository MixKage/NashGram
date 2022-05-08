<template>
  <div class="perinfopage">
    <v-card class="perinfopage__form">
      <v-card-title>Заполните персональные данные пожалуйста</v-card-title>
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
        <v-card-text class="text-h6">Выберете страну</v-card-text>
        <v-select
          v-model="country"
          :items="countries"
          menu-props="auto"
          label=""
          hide-details
          prepend-icon="mdi-map"
          single-line
          @change="handleCountry"
        ></v-select>
        <v-card-text class="text-h6">Выберете дату рождения</v-card-text>
        <v-date-picker
          @change="calculateAge"
          v-model="date_of_bir"
        ></v-date-picker>
        <v-card-text class="text-h5">Возраст:{{ this.age }}</v-card-text>
      </v-form>
      <v-card-text>*-обязательные поля</v-card-text>
      <v-card-actions class="perinfopage__buttons">
        <v-btn color="error" class="ma-auto" @click="reset">
          Сбросить заполнение
        </v-btn>
        <v-btn
          color="green darken-1"
          class="ma-auto"
          text
          @click="change"
          v-if="valid"
        >
          Заполнить
        </v-btn>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script>
import { HTTP } from "../api/API";

export default {
  data: () => ({
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
    handleCountry() {
      this.countryNumber = require("iso-3166-1").whereCountry(
        `${this.country}`
      ).numeric;
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
          this.$router.push("/");
        })
        .catch((err) => {
          console.log(err);
        });
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
      this.name = `User:${this.$store.getters.GET_CURRUSER.id}`;
      this.email = `User${this.$store.getters.GET_CURRUSER.id}@mail.temp`;
      this.countryNumber = require("iso-3166-1").whereCountry(
        `${this.country}`
      ).numeric;
      this.status = `Пусто`;
      this.age = 0;
      this.number = "8005553535";
    },
    async setActualData() {
      console.log(this.$store.getters.GET_CURRUSER);
      console.log(
        require("iso-3166-1").whereNumeric(this.countryNumber).country
      );
      this.name = this.$store.getters.GET_CURRUSER.name;
      this.email = this.$store.getters.GET_CURRUSER.email;
      this.countryNumber = this.$store.getters.GET_CURRUSER.country;
      this.country = require("iso-3166-1").whereNumeric(
        this.countryNumber
      ).country;
      this.status = this.$store.getters.GET_CURRUSER.status;
      this.age = this.$store.getters.GET_CURRUSER.age;
      this.number = this.$store.getters.GET_CURRUSER.number;
    },
  },
  watch: {
    "$store.state.curruser": {
      handler: function () {
        this.setData();
      },
      immediate: true, // provides initial (not changed yet) state
    },
  },
  mounted() {
    this.setCountries();
    this.setActualData();
  },
};
</script>

<style scoped>
.perinfopage__form {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 60%;
  margin: 20% auto;
  text-align: center;
}
.perinfopage__buttons {
  display: flex;
  flex-direction: column;
}
</style>
