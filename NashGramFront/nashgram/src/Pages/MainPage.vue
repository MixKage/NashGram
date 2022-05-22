<template>
  <div id="mpage">
    <Header />
    <v-btn
      v-if="this.$store.getters.GET_AUTH"
      color="black"
      @click="dialog"
      small
      class="add__photo"
      ><v-icon color="white">mdi-plus</v-icon></v-btn
    >
    <v-text-field
      class="mpage__search"
      append-icon="mdi-magnify"
      v-model="search"
    ></v-text-field>
    <PhotoCards v-bind:getposts="getposts" v-bind:photos="filteredList" />
    <PostForm v-bind:getposts="getposts" />
  </div>
</template>
<script>
import Header from "../components/Header";
import PhotoCards from "../components/PhotoCards";
import PostForm from "../components/PostForm";
import { HTTP } from "../api/API";

export default {
  components: {
    Header,
    PhotoCards,
    PostForm,
  },
  data() {
    return {
      search: "",
      photos: [],
    };
  },
  computed: {
    filteredList() {
      return this.photos.filter((photo) => {
        return photo.tag.toLowerCase().includes(this.search.toLowerCase());
      });
    },
  },
  methods: {
    dialog() {
      this.$store.dispatch("SET_DIALOG", !this.$store.getters.GET_DIALOG);
    },
    getposts() {
      HTTP.get("GetAllPosts")
        .then((res) => {
          this.$store.dispatch("SET_POSTS", res.data);
          this.photos = this.$store.getters.GET_POSTS;
        })
        .catch((err) => {
          console.log(err);
          this.err = true;
        });
    },
  },
  mounted() {
    this.getposts();
  },
};
</script>

<style scoped>
.mpage__search {
  padding: 0px 40px;
}
.add__photo {
  position: absolute;
  left: 48%;
  top: 45px;
  border-radius: 50px;
}
.add__photo_fixed {
  position: absolute;
  left: 5%;
  top: 40px;
  z-index: 0;
}
</style>
