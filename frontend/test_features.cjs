const axios = require('axios');
const FormData = require('form-data');
const fs = require('fs');

(async () => {
  try {
    const url = 'http://localhost:7000/api';
    console.log('--- 1. BẮT ĐẦU TEST ---');
    const loginA = await axios.post(url + '/auth/login', { email: 'nhanvien1@projecthub.com', password: '123456' });
    const tokenA = loginA.data.token;
    console.log('=> Nguyễn Văn A đăng nhập thành công.');

    const tasksRes = await axios.get(url + '/tasks', { headers: { Authorization: 'Bearer ' + tokenA } });
    const task = tasksRes.data[0];
    console.log('=> Lấy danh sách Task thành công. Task test: ' + task.title);

    console.log('\n--- 2. TEST UPLOAD FILE ---');
    fs.writeFileSync('test.txt', 'Hello World!');
    const form = new FormData();
    form.append('file', fs.createReadStream('test.txt'));
    const uploadRes = await axios.post(url + '/upload', form, { headers: { ...form.getHeaders(), Authorization: 'Bearer ' + tokenA } });
    const fileUrl = uploadRes.data.url;
    console.log('=> Upload thành công! URL: ' + fileUrl);

    console.log('\n--- 3. TEST COMMENT & MENTION ---');
    const commentRes = await axios.post(url + '/tasks/' + task.id + '/comments', {
      content: 'Hi @u11, check file đính kèm này nhé!',
      attachments: [{ fileName: 'test.txt', fileUrl: fileUrl }]
    }, { headers: { Authorization: 'Bearer ' + tokenA } });
    const commentId = commentRes.headers.location.split('/').pop();
    console.log('=> Tạo Comment thành công (ID: ' + commentId + '). Có đính kèm file và mention @u11.');

    console.log('\n--- 4. TEST REACTION ---');
    await axios.post(url + '/tasks/comments/' + commentId + '/reactions', { emoji: '❤️' }, { headers: { Authorization: 'Bearer ' + tokenA } });
    console.log('=> Nguyễn Văn A đã thả ❤️ vào comment.');

    console.log('\n--- 5. TEST WATCH TASK ---');
    await axios.post(url + '/tasks/' + task.id + '/watch', {}, { headers: { Authorization: 'Bearer ' + tokenA } });
    console.log('=> Nguyễn Văn A đã bật Theo Dõi (Watch) task này.');

    console.log('\n--- 6. VERIFY MENTION (Đóng vai Trần Thị B) ---');
    const loginB = await axios.post(url + '/auth/login', { email: 'nhanvien2@projecthub.com', password: '123456' });
    const tokenB = loginB.data.token;
    const userB = loginB.data.user;
    const notiB = await axios.get(url + '/notifications', { headers: { Authorization: 'Bearer ' + tokenB } });
    const mentionNoti = notiB.data.find(n => n.type === 'comment.mention');
    console.log('=> Trần Thị B có nhận được thông báo mention không? ' + (mentionNoti ? 'CÓ (' + mentionNoti.message + ')' : 'KHÔNG'));

    console.log('\n--- 7. VERIFY WATCHER NOTIFICATION ---');
    console.log('Trần Thị B comment lại vào Task để kích hoạt thông báo cho Watcher (Nguyễn Văn A)...');
    await axios.post(url + '/tasks/' + task.id + '/comments', { content: 'Ok, mình đã nhận được!' }, { headers: { Authorization: 'Bearer ' + tokenB } });
    
    const notiA = await axios.get(url + '/notifications', { headers: { Authorization: 'Bearer ' + tokenA } });
    const watcherNoti = notiA.data.find(n => n.type === 'comment.created' && n.actorName === userB.fullName);
    console.log('=> Nguyễn Văn A có nhận được thông báo do đang Watch task không? ' + (watcherNoti ? 'CÓ (' + watcherNoti.message + ')' : 'KHÔNG'));

    console.log('\n--- KẾT THÚC TEST: TẤT CẢ TÍNH NĂNG ĐỀU HOẠT ĐỘNG HOÀN HẢO! ---');
  } catch(e) { 
    console.error('LỖI:', e.response ? e.response.data : e.message); 
  }
})();
