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
          label="Описание"
          @change="validate"
        ></v-text-field>
      </v-form>
      <v-file-input
        class="postform__file-imput"
        v-model="file"
        value="1"
        accept="image/*"
        label="Фото"
        @change="handleImage"
      ></v-file-input>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="red darken-1" text @click="close"> Отмена </v-btn>
        <v-btn color="green darken-1" text @click="post"> Запостить </v-btn>
        <v-btn color="error" class="mr-4" @click="reset">
          Сбросить заполнение
        </v-btn>
      </v-card-actions>
    </v-card>
    <ErrorDialog></ErrorDialog>
  </v-dialog>
</template>
<script>
import { HTTP } from "../api/API";
import ErrorDialog from "./ErrorDialog";

export default {
  components: { ErrorDialog },
  data: () => ({
    valid: true,
    dialog: false,
    result: "ошибка",
    tag: "",
    description: "",
    file: [],
    imgurl: { url: "" },
    nameRules: [
      (v) => !!v || "Тег необходим",
      (v) => (v && v.length <= 10) || "Тег должен быть меньше  10 символов",
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
    post() {
      const token = localStorage.getItem("token");
      console.log(token);
      HTTP.post(
        "CreatePost",
        {
          image: `${this.imgurl.url}`,
          descryption: `${this.description}`,
          tag: `${this.tag}`,
          idAuthor: `${this.$store.getters.GET_CURRUSER.id}`,
        },
        {
          headers: {
            Authorization: `Basic ${token}`,
          },
        }
      )
        .then(() => {
          this.close();
          this.getposts();
        })
        .catch(() => {
          this.$store.dispatch("SET_ERRDIALOG", true);
        });
    },
    close() {
      this.$store.dispatch("SET_DIALOG", false);
    },
  },
  watch: {
    "$store.state.dialog": {
      handler: function () {
        this.dialog = this.$store.getters.GET_DIALOG;
      },
      immediate: true, // provides initial (not changed yet) state
    },

  },
  props: {
    getposts: {
      required: true,
    },
  },
};
</script>
<style scoped>
.postform {
  padding: 50px;
}
.postform__file-imput {
  padding: 10px 50px;
}
#postform__result {
  margin: 10px auto;
}
</style>
