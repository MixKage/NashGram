<template>
  <div class="regpage">
    <v-card class="regpage__form">
      <h1>NashGram</h1>
      <v-card-title>Регистрация</v-card-title>
      <v-form class="postform" ref="form" v-model="valid" lazy-validation @submit="register">
        <v-text-field
          v-model="login"
          :rules="nameRules"
          label="Логин"
          required
          @change="validate"
        ></v-text-field>

        <v-text-field
          v-model="password"
          :rules="passwordRules"
          label="Пароль"
          required
          @change="validate"
        ></v-text-field>
      </v-form>
      <v-card-actions class="regpage__buttons">
        <v-spacer></v-spacer>
        <v-btn color="green darken-1" text @click="register"> Зарегистрироваться </v-btn>
        <v-btn color="error" class="mr-4" @click="reset">
          Сбросить заполнение
        </v-btn>
        <v-card-text>
          Зарегистрированы?
          <router-link class="link" :to="{ path: '/login' }">Войти</router-link>
        </v-card-text>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script>
import { HTTP } from "../api/API";

export default {
  data: () => ({
    valid: true,
    login: "",
    password: "",
    nameRules: [
      (v) => !!v || "Логин необходим",
      (v) => (v && v.length <= 50) || "Логин должен быть меньше 50  символов",
    ],
    passwordRules: [
      (v) => !!v || "Пароль необходим",
      (v) => (v && v.length >= 8) || "Пароль должен быть не меньше 8 символов",
    ],
  }),

  methods: {
    validate() {
      this.$refs.form.validate();
    },
    reset() {
      this.$refs.form.reset();
    },
    register() {
      HTTP.post("CreateAccount", {
        login: `${this.login}`,
        password: `${this.password}`,
      })
        .then((res) => {
          console.log(res.data);
          this.$router.push("/login");
        })
        .catch((err) => {
          console.log(err);
          this.err = true;
        });

    },
  },
};
</script>

<style scoped>
.regpage__form {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 60%;
  margin: 20% auto;
  text-align: center;
}
.regpage__buttons {
  display: flex;
  flex-direction: column;
}
</style>
