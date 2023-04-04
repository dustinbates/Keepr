<template>
  <div class="keepCard bg-white rounded">
    <img @click="setActiveKeep(keep.id)" data-bs-toggle="modal" data-bs-target="#activeKeep"
      class="keepImg img-fluid rounded" :src="keep.img" :alt="keep.name">
    <p class="overlay">{{ keep.name }}</p>
    <router-link :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
      <img class="creatorImg overlayBottomRight" :src="keep.creator.picture" :alt="keep.creator.name">
    </router-link>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { computed } from 'vue';

export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep),
      setActiveKeep(keepId) {
        keepsService.setActiveKeep(keepId)
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.keepCard {
  position: relative;
}

.keepImg {
  box-shadow: 0px 6px 5px rgba(0, 0, 0, 0.478);
}

.creatorImg {
  height: 40px;
  width: 40px;
  border-radius: 50%;
}

.overlay {
  position: absolute;
  text-align: start;
  font-family: fantasy;
  font-size: x-large;
  margin: .5em;
  bottom: 0;
  color: rgb(240, 234, 244);
  text-shadow: 1px 1px 2px rgb(0, 0, 0);
  width: 50%;
}

.overlayBottomRight {
  position: absolute;
  text-align: end;
  margin: .5em;
  bottom: 0;
  right: 0;
  box-shadow: 1px 1px 5px rgb(0, 0, 0);
}
</style>