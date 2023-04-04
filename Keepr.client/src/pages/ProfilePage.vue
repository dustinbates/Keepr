<template>
  <div class="about text-center">
    <h1>Welcome {{ profile.name }}</h1>
    <img class="rounded" :src="profile.picture" alt="" />
    <p>{{ profile.email }}</p>
  </div>
  <section class="bricks">
    <div v-for="k in keeps">
      <KeepCard :keep="k" />
    </div>
  </section>
  <Modal id="activeKeep">
    <ActiveKeep />
  </Modal>
  <Modal id="createKeep">
    <CreateKeepForm />
  </Modal>
  <Modal id="createVault">
    <CreateVaultForm />
  </Modal>
</template>

<script>
import { computed, onMounted, watchEffect } from 'vue'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { useRoute, useRouter } from 'vue-router';

export default {
  setup() {
    const route = useRoute();
    const router = useRouter();
    onMounted(() => {
      getProfileById();
    })
    watchEffect(() => {
      getKeeps();
    })
    async function getProfileById() {
      try {
        const profileId = route.params.profileId
        await profilesService.getProfileById(profileId);
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
      }
    }
    async function getKeeps() {
      try {
        const id = AppState.profile.id
        await keepsService.getProfileKeeps(id)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    return {
      profile: computed(() => AppState.profile),
      keeps: computed(() => AppState.profileKeeps),
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  max-width: 100px;
}

.bricks {
  columns: 4 200px;
  column-gap: 1.5rem;
  margin-right: 5rem;
  margin-left: 5rem;

  @media screen and (max-width: 456px) {
    columns: 2;
    margin-right: .25rem;
    margin-left: .25rem;
  }

  &>div {
    width: 150px;
    color: white;
    margin: 0 1.5rem 1.5rem 0;
    display: inline-block;
    width: 100%;
  }

}
</style>
