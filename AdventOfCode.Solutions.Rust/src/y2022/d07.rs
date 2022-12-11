use std::collections::HashMap;

use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> String {
    match part {
        Part::P1 => Day07::solve_part_one(input),
        Part::P2 => Day07::solve_part_two(input),
    }
}

const MAX_DIR_SIZE: i32 = 100_000;
const SYSTEM_SPACE: i32 = 70_000_000;
const UPDATES_SIZE: i32 = 30_000_000;

struct Dir {
    size: i32,
    dirs: Vec<String>,
}

impl Dir {
    fn total_size(&self, dirs: &HashMap<String, Dir>) -> i32 {
        self.dirs.iter().fold(self.size, |total, key| {
            total + dirs[key].total_size(dirs)
        })
    }
}

struct Day07;
impl Solution for Day07 {
    fn solve_part_one(input: String) -> String {
        let mut path = vec![];
        let directories: HashMap<String, Dir> = input
            .split("$ cd ")
            .filter_map(|section| {
                let mut lines = section.lines();
                let dir = match lines.next() {
                    None => return None,
                    Some("..") => {
                        path.pop();
                        return None
                    },
                    Some(dir)  => dir,
                };

                path.push(dir);
                let cwd = path.join("");

                let mut size = 0;
                let mut dirs = Vec::new();
                for line in lines.skip(1) {
                    let (first, second) = line.split_once(' ').unwrap();

                    if let Ok(file_size) = first.parse::<i32>() {
                        size += file_size;
                    } else {
                        dirs.push(format!("{}{}", cwd, second));
                    }
                }

                Some((cwd, Dir { size, dirs }))
            })
            .collect();

        directories
            .values()
            .map(|dir| dir.total_size(&directories))
            .filter(|size| size < &MAX_DIR_SIZE)
            .sum::<i32>()
            .to_string()
    }

    fn solve_part_two(input: String) -> String {
        let mut path = vec![];
        let directories: HashMap<String, Dir> = input
            .split("$ cd ")
            .filter_map(|section| {
                let mut lines = section.lines();
                let dir = match lines.next() {
                    None => return None,
                    Some("..") => {
                        path.pop();
                        return None
                    },
                    Some(dir)  => dir,
                };

                path.push(dir);
                let cwd = path.join("");

                let mut size = 0;
                let mut dirs = Vec::new();
                for line in lines.skip(1) {
                    let (first, second) = line.split_once(' ').unwrap();

                    if let Ok(file_size) = first.parse::<i32>() {
                        size += file_size;
                    } else {
                        dirs.push(format!("{}{}", cwd, second));
                    }
                }

                Some((cwd, Dir { size, dirs }))
            })
            .collect();

        let needed_space = UPDATES_SIZE - (SYSTEM_SPACE - directories["/"].total_size(&directories));
        directories
            .values()
            .filter_map(|dir| {
                let size = dir.total_size(&directories);
                if size >= needed_space {
                    Some(size)
                } else {
                    None
                }
            })
            .min()
            .unwrap()
            .to_string()
    }
}
