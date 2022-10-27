var editors = document.querySelectorAll(".editor");
if (editors.length) {
    console.log("ali")
    $.getScript("/common/ckeditor/build/ckeditor.js", function (data, textStatus, jqxhr) {
        for (editor of editors) {
            ClassicEditor
                .create(editor, {

                    licenseKey: '',
                    simpleUpload: {
                        // The URL that the images are uploaded to.
                        uploadUrl: '/Home/UploadCkeditor'
                    }
                })
                .then(editor => {
                    window.editor = editor;
                })
                .catch(error => {
                    console.log(error);
                });
        }
    });

}