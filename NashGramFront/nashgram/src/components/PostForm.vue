<template>
  <v-dialog v-model="dialog" persistent max-width="80%">
    <v-card>
      <v-card-title class="text-h5"> Хотите запостить фото? </v-card-title>
      <v-form class="postform" ref="form" v-model="valid" lazy-validation>
        <v-text-field
          v-model="tag"
          :counter="10"
          :rules="nameRules"
          label="Тэг"
          required
          @change="validate"
        ></v-text-field>

        <v-text-field
          v-model="description"
          :counter="500"
          :rules="descRules"
          label="Описание"
          required
          @change="validate"
        ></v-text-field>
        <v-text-field
          v-model="URL"
          :rules="urlRules"
          label="Ссылка"
          required
          @change="validate"
        ></v-text-field>
        <v-checkbox
          v-model="checkbox"
          :rules="[(v) => !!v || 'Вы должны подтвердить']"
          label="Уверены ?"
          required
        ></v-checkbox>
      </v-form>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="red darken-1" text @click="dialog = !dialog">
          Отмена
        </v-btn>
        <v-btn color="green darken-1" text @click="dialog = !dialog">
          Запостить
        </v-btn>
        <v-btn color="error" class="mr-4" @click="reset"> Сбросить заполнение </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script>
export default {
  data: () => ({
    valid: true,
    tag: "",
    description: "",
    URL: "",
    nameRules: [
      (v) => !!v || "Тег необходим",
      (v) => (v && v.length <= 10) || "Тег должен быть меньше  10 символов",
    ],
    descRules: [
      (v) => !!v || "Описание необходимо",
      (v) =>
        (v && v.length <= 500) || "Описание должно быть меньше 500 символов",
    ],
    urlRules: [
      (v) => !!v || "Описание необходимо",
      (v) =>
        (v && v.length <= 500) || "Описание должно быть меньше 500 символов",
    ],
    select: null,
    checkbox: false,
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
.postform {
  padding: 50px;
}
</style>
