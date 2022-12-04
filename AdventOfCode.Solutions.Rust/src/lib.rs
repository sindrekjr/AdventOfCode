use std::ffi::CString;
use std::os::raw::c_char;

#[no_mangle]
pub extern fn solve(year: i32, day: i32, part: i32, ptr: *mut c_char) -> *mut c_char {
    let input = unwrap_input(ptr);

    println!(
        "{}",
        format!(
            "Hello from Rust! Solving AoC{} Day {} Part {}.\nInput: {}",
            year, day, part, input
        )
    );

    return CString::new(input).unwrap().into_raw();
}

fn unwrap_input(ptr: *mut c_char) -> String {
    unsafe {
        match CString::from_raw(ptr).into_string() {
            Ok(inp) => inp,
            Err(why) => why.to_string()
        }
    }
}
