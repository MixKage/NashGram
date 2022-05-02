<template>
  <div class="logpage">
    <v-card class="logpage__form">
      <h1>NashGram</h1>
      <v-card-title>Вход</v-card-title>
      <v-form
        class="postform"
        ref="form"
        v-model="valid"
        lazy-validation
        @submit="login"
      >
        <v-text-field
          v-model="username"
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
      <v-card-actions class="logpage__buttons">
        <v-spacer></v-spacer>
        <v-btn color="green darken-1" text @click="login"> Войти </v-btn>
        <v-btn color="error" class="mr-4" @click="reset">
          Сбросить заполнение
        </v-btn>
        <v-card-text>
          Впервые тут?
          <router-link class="link" :to="{ path: '/register' }"
            >Зарегистрироваться</router-link
          >
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
    username: "",
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
    login() {
      const token = Buffer.from(
        `${this.username}:${this.password}`,
        "utf8"
      ).toString("Base64");
      HTTP.get("GetPerson", {
        headers: {
          Authorization: `Basic ${token}`,
        },
      })
        .then((res) => {
          this.$router.push("/perinfo");
          this.$store.dispatch("SET_AUTH", true);
          this.$store.dispatch("SET_CURRUSER", res.data);
          console.log(this.$store.getters.GET_AUTH);
          localStorage.setItem("token", token);
          console.log(localStorage.getItem("token"));
        })
        .catch((err) => {
          console.log(`${this.username}:${this.password}`);

          this.$store.dispatch("SET_AUTH", false);
          console.log(this.$store.getters.GET_AUTH);
          localStorage.setItem("token", "");
          console.log(err);
          this.err = true;
        });
    },

  },
};
</script>

<style scoped>
.logpage__form {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 60%;
  margin: 20% auto;
  text-align: center;
}
.logpage__buttons {
  display: flex;
  flex-direction: column;
}
</style>
