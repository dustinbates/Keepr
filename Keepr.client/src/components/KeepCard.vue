<template>
  <div class="keepCard bg-white rounded elevation-1">
    <img class="keepImg img-fluid rounded" :src="keep.img" alt="">
    <h4 class="overlay">{{ keep.name }}</h4>


  </div>
</template>


<script>
export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {}
  }
}
</script>


<style lang="scss" scoped>
.keepCard {
  position: relative;
}

.keepImg {
  box-shadow: 1px 1px 10px rgba(0, 0, 0, 0.478);
}

.overlay {
  position: absolute;
  text-align: start;
  margin: .5em;
  bottom: 0;
  color: rgb(240, 234, 244);
  text-shadow: 1px 1px 2px rgb(0, 0, 0);
  width: 50%;
}
</style>