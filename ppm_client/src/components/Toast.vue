<script setup>

//TODO(Setup toast)
const props = defineProps({
    status: String,
    message: String,
})
</script>

<template>
    <div class="toast toast-center">
        <div class="alert" :class="`alert-${status}`">
            <span>{{ message }}</span>
        </div>
    </div>
</template>

<style scoped>
</style>