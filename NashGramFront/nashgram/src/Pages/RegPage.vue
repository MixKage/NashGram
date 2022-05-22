<template>
  <div class="regpage">
    <v-card class="regpage__form">
      <v-carousel
        class="regpage__carusel"
        cycle
        hide-delimiters
        :show-arrows="false"
      >
        <v-carousel-item
          v-for="(item, i) in items"
          :key="i"
          :src="item.src"
          reverse-transition="fade-transition"
          transition="fade-transition"
        ></v-carousel-item>
      </v-carousel>
      <div class="form">
        <h1>NashGram</h1>
        <v-card-title>Регистрация</v-card-title>
        <v-form
          class="postform"
          ref="form"
          v-model="valid"
          lazy-validation
          @submit="register"
        >
          <v-text-field
            v-model="login"
            :rules="nameRules"
            label="Логин"
            required
            @change="validate"
          ></v-text-field>

          <v-text-field
            v-model="password"
            :append-icon="showpass ? 'mdi-eye' : 'mdi-eye-off'"
            :type="showpass ? 'text' : 'password'"
            :rules="passwordRules"
            counter
            @click:append="showpass = !showpass"
            label="Пароль"
            required
            @change="validate"
          ></v-text-field>
        </v-form>
        <v-btn color="red darken-1" text @click="reset">
          Сбросить заполнение
        </v-btn>
        <v-card-actions class="regpage__buttons">
          <v-btn color="green darken-1" text @click="register">
            Зарегистрироваться
          </v-btn>
          <v-spacer></v-spacer>
          <v-card-text>
            Впервые тут?
            <router-link class="link" :to="{ path: '/login' }"
              >Войти</router-link
            >
          </v-card-text>
        </v-card-actions>
      </div>
    </v-card>
    <v-dialog v-model="errDialog" persistent max-width="290">
      <v-card>
        <v-card-title class="text-h5"> Что-то не так... </v-card-title>
        <v-card-text>Проблема с сетью или заполнением данных</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="green darken-1" text @click="close"> Ок </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>
import { HTTP } from "../api/API";

export default {
  data: () => ({
    items: [
      {
        src: "https://cdn.vuetifyjs.com/images/carousel/squirrel.jpg",
      },
      {
        src: "https://cdn.vuetifyjs.com/images/carousel/sky.jpg",
      },
      {
        src: "https://cdn.vuetifyjs.com/images/carousel/bird.jpg",
      },
      {
        src: "https://cdn.vuetifyjs.com/images/carousel/planet.jpg",
      },
    ],
    errDialog: false,
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
    close() {
      this.errDialog = false;
    },
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
          this.errDialog = true;
        });
    },
  },
};
</script>

<style scoped>
.postform {
  padding: 10px;
  margin: 30px auto;
}
.regpage {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
}
.regpage__form {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  text-align: center;
  width: 90%;
}
.regpage__buttons {
  display: flex;
  flex-direction: column;
}
</style>
