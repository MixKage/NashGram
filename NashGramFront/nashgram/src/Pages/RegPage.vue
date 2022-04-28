<template>
  <div class="regpage">
    <v-card class="regpage__form">
      <h1>NashGram</h1>
      <v-card-title>Регистрация</v-card-title>
      <v-form class="postform" ref="form" v-model="valid" lazy-validation>
        <v-text-field
          v-model="name"
          :counter="50"
          :rules="nameRules"
          label="Логин"
          required
          @change="validate"
        ></v-text-field>

        <v-text-field
          v-model="password"
          :counter="8"
          :rules="passwordRules"
          label="Пароль"
          required
          @change="validate"
        ></v-text-field>
      </v-form>
      <v-card-actions class="regpage__buttons">
        <v-spacer></v-spacer>
        <v-btn color="green darken-1" text> Зарегистрироваться </v-btn>
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
export default {
  data: () => ({
    valid: true,
    name: "",
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
  },
  props: {
    dialog: {
      required: true,
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
