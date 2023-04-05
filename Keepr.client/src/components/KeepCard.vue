<template>
  <div class="keepCard bg-white rounded">
    <i v-if="account.id == keep.creatorId"
      class="fs-5 p-0 m-0 mdi mdi-delete-outline bg-danger rounded-pill overlayTopRight" @click="deleteKeep(keep.id)"></i>
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
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';

export default {
  props: { keep: { type: Object, required: true } },
  setup() {
    return {
      account: computed(() => AppState.account),
      keeps: computed(() => AppState.keeps),
      activeKeep: computed(() => AppState.activeKeep),
      setActiveKeep(keepId) {
        try {
          keepsService.setActiveKeep(keepId)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      },
      async deleteKeep(keepId) {
        try {
          if (await Pop.confirm('Delete this Keep?', 'Are you sure you want to delete this Keep? This will be removed from other Vaults', 'Delete', 'question')) {
            keepsService.deleteKeep(keepId)
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
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
  overflow: hidden;
  text-overflow: ellipsis;
}

.overlayBottomRight {
  position: absolute;
  text-align: end;
  margin: .5em;
  bottom: 0;
  right: 0;
  box-shadow: 1px 1px 5px rgb(0, 0, 0);
}

.overlayTopRight {
  position: absolute;
  text-align: center;
  height: 30px;
  width: 30px;
  top: 0;
  right: 0;
  box-shadow: 1px 1px 5px rgb(0, 0, 0);
}
</style>