<template>
  <div class="keepCard bg-white rounded selectable">
    <i v-if="account.id == keep.creatorId"
      class="fs-5 p-0 m-0 mdi mdi-delete-outline bg-danger rounded-pill overlayTopRight" title="Delete Keep"
      @click="deleteKeep(keep.id)"></i>
    <img @click="setActiveKeep(keep)" data-bs-toggle="modal" data-bs-target="#activeKeep"
      class="keepImg img-fluid rounded" title="View Keep" :src="keep.img" :alt="keep.name"
      @error="brokenLink(keep, 'keepImg')">
    <p class="overlay">{{ keep.name }}</p>
    <router-link :to="{ name: 'Profile', params: { profileId: keep.creator.id } }">
      <img class="creatorImg overlayBottomRight" :title="keep.creator.name" :src="keep.creator.picture"
        :alt="keep.creator.name" @error="brokenLink(keep, 'profileImg')">
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
      async setActiveKeep(keep) {
        try {
          await keepsService.setActiveKeep(keep)
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
      },
      brokenLink(keep, problem) {
        if (problem == "keepImg") {
          keep.img = 'https://i.natgeofe.com/n/548467d8-c5f1-4551-9f58-6817a8d2c45e/NationalGeographic_2572187_square.jpg'
        } else if (problem == "profileImg") {
          keep.creator.picture = 'https://images.unsplash.com/photo-1611915387288-fd8d2f5f928b?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8&w=1000&q=80'
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
  width: 100%;
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