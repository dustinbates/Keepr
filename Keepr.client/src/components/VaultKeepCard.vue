<template>
  <div class="keepCard bg-white rounded selectable">
    <img @click="setActiveKeep(vaultKeep)" data-bs-toggle="modal" data-bs-target="#activeKeep"
      class="keepImg img-fluid rounded" :src="vaultKeep.img" :alt="vaultKeep.name"
      @error="brokenLink(vaultKeep, 'keepImg')">
    <p class="overlay">{{ vaultKeep.name }}</p>
    <router-link :to="{ name: 'Profile', params: { profileId: vaultKeep.creator.id } }">
      <img class="creatorImg overlayBottomRight" :src="vaultKeep.creator.picture" :alt="vaultKeep.creator.name"
        @error="brokenLink(vaultKeep, 'profileImg')">
    </router-link>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { keepsService } from '../services/KeepsService';
import { computed } from 'vue';

export default {
  props: { vaultKeep: { type: Object, required: true } },
  setup() {
    return {
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vaults),
      vaultKeeps: computed(() => AppState.vaultKeeps),
      activeKeep: computed(() => AppState.activeKeep),
      setActiveKeep(vaultKeep) {
        keepsService.setActiveKeep(vaultKeep)
      },
      brokenLink(vaultKeep, problem) {
        if (problem == "keepImg") {
          vaultKeep.img = 'https://i.natgeofe.com/n/548467d8-c5f1-4551-9f58-6817a8d2c45e/NationalGeographic_2572187_square.jpg'
        } else if (problem == "profileImg") {
          vaultKeep.creator.picture = 'https://images.unsplash.com/photo-1611915387288-fd8d2f5f928b?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxleHBsb3JlLWZlZWR8MXx8fGVufDB8fHx8&w=1000&q=80'
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
  text-align: end;
  margin: .5em;
  top: 0;
  right: 0;
  box-shadow: 1px 1px 5px rgb(0, 0, 0);
}
</style>