<template>
  <div id="mpage">
    <Header />
    <v-btn color="black" @click="dialog = !dialog" small class="add__photo"
      ><v-icon color="white">mdi-plus</v-icon></v-btn
    >
    <PhotoCards v-bind:photos="photos" />
    <PostForm v-bind:dialog="dialog" />
  </div>
</template>
<script>
import Header from "../components/Header";
import PhotoCards from "../components/PhotoCards";
import PostForm from "../components/PostForm";
export default {
  components: {
    Header,
    PhotoCards,
    PostForm
  },
  data() {
    return {
      dialog: false,
      photos: [
        {
          id: 1,
          author: 2,
          uri: "https://c.wallhere.com/photos/fc/9a/1366x768_px_Canada_landscape_mountain_stars_Trees-1080526.jpg!d",
          description: "Фото леса",
          tag: "Природа",
          is_liked: false,
          likes: 1,
        },
        {
          id: 2,
          author: 2,
          uri: "https://c.wallhere.com/photos/fc/9a/1366x768_px_Canada_landscape_mountain_stars_Trees-1080526.jpg!d",
          description: "Фото леса",
          tag: "Природа",
          is_liked: true,
          likes: 3,
        },
        {
          id: 3,
          author: 2,
          uri: "https://c.wallhere.com/photos/fc/9a/1366x768_px_Canada_landscape_mountain_stars_Trees-1080526.jpg!d",
          description: "Фото леса",
          tag: "Природа",
          is_liked: false,
          likes: 5,
        },
      ],
    };
  },
};
</script>

<style scoped>
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
