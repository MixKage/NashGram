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
        <v-text-field
          v-model="country"
          label="Страна"
          @change="validate"
        ></v-text-field>
        <v-text-field
          v-model="age"
          label="Дата рождения"
          @change="validate"
          class="perinfopage__age"
          readonly
        ></v-text-field>
        <v-date-picker v-model="age"></v-date-picker>
      </v-form>
      <v-card-text>*-обязательные поля</v-card-text>
      <v-card-actions class="perinfopage__buttons">
        <v-spacer></v-spacer>
        <v-btn color="error" class="mr-4" @click="reset">
          Сбросить заполнение
        </v-btn>
        <v-btn color="green darken-1" text @click="change" v-if="valid"> Заполнить </v-btn>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script>
export default {
  data: () => ({
    valid: false,
    name: "",
    email: "",
    country: "",
    age: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
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
      this.$refs.form.reset();
    },
    change() {},
  },
  mounted() {
    this.validate();
  }
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
